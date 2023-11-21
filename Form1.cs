using System.Text;

namespace kmumed_pk_grouper
{
    public partial class MainForm : Form
    {
        private List<Node> population;
        public static int mode;
        private int ppt;
        public int personPerTeam
        {
            get { return ppt; }
            set 
            { 
                ppt = value;
                WhenNoSingleModeSettingChanged();
            }
        }
        public static int genderRatioMode = 0;
        public static bool considerSurgicalCopy = false;
        public static bool whenNoMoreThanHalf_RandomForOddsTeam = false;
        bool isLoggingOn = true;
        List<Tuple<int, int, int>> surgicalCopyComposition; //���� �ο���, �ش� �� ����, A copy limit�� ��� / !!!!Count �ִ� 2��!!!!

        public MainForm()
        {
            InitializeComponent();
        }

        private void DoLottery() {
            AppendLineWithTimestamp("��÷�� ���� ���α׷� �ʱ�ȭ ����.", true);
            //calculate minor gender
            int maleCount = population.Where(n => n.gender.Equals('M')).Count();
            char minorGender;
            if (maleCount > Math.Floor((decimal)population.Count / 2))
                minorGender = 'F';
            else
                minorGender = 'M';
            AppendLineWithTimestamp($"����: {maleCount}�� / ���� {population.Count - maleCount}��", true);
            if(genderRatioMode != 0)
                AppendLog($"�Ҽ��� ���� {minorGender} �̸�, ���� ����Ͽ� ��÷�մϴ�", true);

            //initialize Groups: �� ABī�� �� ���� ��������
            List<Group> groups = new List<Group>();
            calculateACopyLimits();
            foreach (Tuple<int, int, int> t in surgicalCopyComposition)
            {
                for (int i = 0; i < t.Item2; i++) {
                    groups.Add(new Group(
                            limit: t.Item1,
                            minorGender,
                            a_limit: t.Item3
                        ));
                }
                AppendLog($"{t.Item1} ���� {t.Item2}���� �����Ǿ����ϴ�.", true);
                if (considerSurgicalCopy)
                    AppendLog($"- A Copy �ִ� {t.Item3}����� �����˴ϴ�", true);
            }

            if(genderRatioMode == 1)
                calculateGenderLimits(groups);

            AppendLineWithTimestamp($"���� {groups.Count}���� ���� �����Ǿ����ϴ�.", true);


            //���� ���� radomize and initialize
            List<int> seq;
            int cycleCount = 0;
            bool printResult = true;
            AppendLineWithTimestamp($"�ʱ�ȭ �Ϸ�. ��÷�� �����մϴ�.", true);
            do
            {
                //cycle���� �ʿ��� �ʱ�ȭ
                cycleCount++;
                printResult = true;
                groups.ForEach(g => g.Reset());
                //cycle ���� ��� ������� randomize
                population = population.OrderBy(x => Random.Shared.Next()).ToList();

                AppendLineWithTimestamp($"{cycleCount}��° cycle ��÷�� �����մϴ�.", false);
                int current = 0;
                foreach (Node n in population)
                {
                    current++;
                    //������� group �õ� ���� randomize
                    seq = GetTeamTrySequence();
                    for (int i = 0; i < groups.Count; i++)
                    {
                        if (groups[seq[i]].Add(n))
                        {
                            AppendLineWithTimestamp($"{cycleCount}-{current}; {n.HandleName}�� {seq[i] + 1}���� �����Ǿ����ϴ�", false);
                            if(isLoggingOn && cycleCount - 1 < loggingUntilCycleNum.Value)
                                Thread.Sleep((int)(loggingDelayNum.Value * 1000));
                            break;
                        }
                        if (i == groups.Count - 1)
                        {
                            AppendLineWithTimestamp("!! Ư������ ��� ���� ���� ������ �� ���� �ٽ� ��÷�ؾ� �մϴ�!!", false);
                            printResult = false;
                        }
                    }
                    if (!printResult)
                        break; //���� �Ұ��� case �߻��� ��� ���� �� ����÷
                }
            } while (!printResult && cycleCount < 100);

            if (printResult)
            {
                AppendLineWithTimestamp($"{cycleCount}ȸ ��÷ ��� ��÷�� �����߽��ϴ�", true);
                StartButton.Enabled = false;
                foreach (Group g in groups)
                {
                    AppendLog(g.Print(true), true);
                }
                SaveCSV(groups);
            }
            else
            {
                AppendLineWithTimestamp($"{cycleCount}ȸ ��÷ ��� ��÷�� �����߽��ϴ�. ���� ��� ������ �����ϰų� �ٽ� �õ����ּ���.", true);
            }
        }

        #region Event handlers
        private void dbFilePath_Click(object sender, EventArgs e)
        {
            string filePath = ShowGeneralFileOpenDialog(
                title: "������ ���� ����",
                initialDirectory: "%HOMEPATH%\\Desktop",
                filter: "��ǥ�� ���е� �� ���� (*.csv)|*.csv",
                defaultExt: "csv"
                );
            if (Path.Exists(filePath))
            {
                dbFilePathTB.Text = filePath;
            }
            else
            {
                MessageBox.Show("������ ���õ��� �ʾҰų� �������� �ʽ��ϴ�. �ٽ� �õ����ּ���.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void totalPopulationNumeric_ValueChanged(object sender, EventArgs e)
        {
            int totalCount = (int)totalPopulationNumeric.Value;
            int teamCount = (int)teamCountNumeric.Value;
            int localPersonPerTeam = (int)Math.Ceiling(totalCount / teamCountNumeric.Value);
            if (totalCount == localPersonPerTeam * teamCount)
            {
                populationPerTeamLabel1.Text = $"{localPersonPerTeam}����";
                populationPerTeamNum1.Value = teamCount;
                populationPerTeamLabel2.Visible = false;
                populationPerTeamNum2.Visible = false;
                populationPerTeamNum2.Value = 0;
                surgicalCopyComposition = new List<Tuple<int, int, int>>
                {
                    new Tuple<int, int, int>(localPersonPerTeam, teamCount, 0)
                };
            }
            else
            {
                populationPerTeamLabel1.Text = $"{localPersonPerTeam - 1}����";
                populationPerTeamLabel2.Text = $"{localPersonPerTeam}����";
                populationPerTeamNum1.Value = teamCount * localPersonPerTeam - totalCount;
                populationPerTeamNum2.Value = teamCount - populationPerTeamNum1.Value;
                populationPerTeamLabel2.Visible = true;
                populationPerTeamNum2.Visible = true;
                surgicalCopyComposition = new List<Tuple<int, int, int>>
                {
                    new Tuple<int, int, int>(localPersonPerTeam - 1, (int)populationPerTeamNum1.Value, 0),
                    new Tuple<int, int, int>(localPersonPerTeam, (int)populationPerTeamNum2.Value, 0)
                };
            }
            personPerTeam = localPersonPerTeam;
        }

        private void consigderSurgicalCopySelected(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                switch ((sender as RadioButton).Tag)
                {
                    case "y":
                        considerSurgicalCopy = true;
                        break;
                    case "n":
                        considerSurgicalCopy = false;
                        break;
                    default:
                        return;
                }
            }
        }

        private void ModeSelected(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                switch ((sender as RadioButton).Tag)
                {
                    case "31":
                        teamCountNumeric.Value = 18;
                        considerSurgicalCopyNoRB.Select();
                        mode = 1;
                        break;
                    case "32":
                        teamCountNumeric.Value = 15;
                        considerSurgicalCopyYesRB.Select();
                        mode = 2;
                        break;
                    case "41":
                        teamCountNumeric.Value = 20;
                        considerSurgicalCopyNoRB.Select();
                        mode = 3;
                        break;
                    default:
                        return;
                }
            }
        }

        private void LoggingSelected(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                switch ((sender as RadioButton).Tag)
                {
                    case "y":
                        isLoggingOn = true;
                        break;
                    case "n":
                        isLoggingOn = false;
                        break;
                    default:
                        return;
                }
                logSettingsPanel.Visible = isLoggingOn;
            }
        }

        private void whenNoSingleRandomRB_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
            {
                switch ((sender as RadioButton).Tag)
                {
                    case "y":
                        whenNoMoreThanHalf_RandomForOddsTeam = true;
                        break;
                    case "n":
                        whenNoMoreThanHalf_RandomForOddsTeam = false;
                        break;
                    default:
                        return;
                }
                logSettingsPanel.Visible = isLoggingOn;
            }
        }

        private void GenderRatioSelected(object sender, EventArgs e)
        {
            if ((sender as RadioButton).Checked)
                genderRatioMode = int.Parse((sender as RadioButton).Tag as string);
            WhenNoSingleModeSettingChanged();
        }

        private void dbFilePathTB_TextChanged(object sender, EventArgs e)
        {
            population = LoadCSV(dbFilePathTB.Text);
            totalPopulationNumeric.Value = population.Count;
            if (population.Where(p => p.gender.Equals('F')).Count() % 2 != 0 || population.Where(p => p.gender.Equals('M')).Count() % 2 != 0)
            {
                genderRatio_NoSingleRB.Enabled = false;
                genderRatio_NoHalfAndSingle.Enabled = false;
            }
            else
            {
                genderRatio_NoSingleRB.Enabled = true;
                genderRatio_NoHalfAndSingle.Enabled = true;
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (!Path.Exists(dbFilePathTB.Text))
            { 
                MessageBox.Show("������ ������ ���õ��� �ʾҰų� �������� �ʽ��ϴ�. �ٽ� �õ����ּ���.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Cursor.Current = Cursors.WaitCursor;
            Group.ResetCumulative();
            DoLottery();
            Cursor.Current = Cursors.Default;
        }
        #endregion

        #region Utility
        /// <summary>
        /// �Ϲ��� �뵵�� ����� �� �ִ� ���� ���� ���̾�α��Դϴ�.
        /// </summary>
        /// <param name="title">��ȭ ������ ���� ǥ���ٿ� ��Ÿ�� ���ڿ��Դϴ�.</param>
        /// <param name="initialDirectory">��ȭ ������ �ʱ� ���͸��Դϴ�. \�� �����ϴ�.</param>
        /// <param name="filename">��ȭ ���ڿ� ó�� ǥ�õ� ���� �Ǵ� ����ڰ� ������ ���� �̸��Դϴ�.</param>
        /// <param name="defaultExt">�⺻ ���� �̸� Ȯ����Դϴ�. ����ڰ� Ȯ����� �Է����� �ʰ� ������ �����ϸ� ���� �� Ȯ����� �߰��˴ϴ�.</param>
        /// <param name="filter">��ȭ ���ڿ� ǥ���� ���� �����Դϴ�(�� : "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*")</param>
        /// <param name="filterIndex">��ȭ ���ڿ��� ������ ���� ������ �ε����Դϴ�. ù ��° �ε����� 1�Դϴ�.</param>
        /// <returns>������ ���� ���(�� : "C:\Users\filename.ext")�� ��ȯ�մϴ�.</returns>
        public static string ShowGeneralFileOpenDialog(string title = "������ �������ּ���", string initialDirectory = "MyComputer", string? filename = null, string? defaultExt = null, string filter = "��� ����(*.*)|*.*", int filterIndex = 1)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = title;
            openFileDialog.InitialDirectory = initialDirectory;
            openFileDialog.FileName = filename;
            openFileDialog.Filter = filter;
            openFileDialog.DefaultExt = defaultExt;
            openFileDialog.FilterIndex = filterIndex;
            DialogResult dr = openFileDialog.ShowDialog();
            //OK��ư Ŭ����
            if (dr == DialogResult.OK)
            {
                //File��ο� File���� ��� ��ȯ
                return openFileDialog.FileName;
            }
            return null;
        }

        List<Node> LoadCSV(string fileName)
        {
            string[] buff = File.ReadAllLines(fileName, Encoding.UTF8);
            List<Node> table = new List<Node>();
            foreach (string d in buff)
            {
                try
                {
                    string[] split = d.Split(',');
                    if (split[0].Equals("�й�")) continue; //ù �� ����
                    table.Add(new Node(split));
                }
                catch (ArgumentException ex)
                {
                    AppendLog($"{ex.Message}", true);
                }
            }

            table.GroupBy(i => i.Name)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key) // �ߺ��Ǵ� �̸� ã�Ƽ�
                .ToList()
                .ForEach(s => 
                    {
                        table.FindAll(n => n.Name.Equals(s)) //Instance�ȿ��� �й� �����ϵ��� ����
                            .ForEach(n => n.Name = $"{n.Name}({n.Id})");
                    }
                );
            return table;
        }

        private void SaveCSV(List<Group> groups)
        {
            AppendLineWithTimestamp($"��÷ ����� �����մϴ�.", true);
            //Header
            int maxPPT = groups.Max(g => g.limit);
            string result = "��,��,��,";
            string[] headerPersons = new string[maxPPT];
            for (int i = 0; i < maxPPT; i++)
                headerPersons[i] = $"����{i + 1}";
            result += $"{string.Join(",", headerPersons)}\n";

            //Body
            groups.OrderBy(g => g.groupNumber);
            groups.ForEach(g => result += $"{g.PrintForCSV()}\n");

            //Save
            string filename = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\�ǽ��� ��÷ ��� - {DateTime.Now.ToString("yyyyMMdd hhmmss")}.csv";
            using (StreamWriter file = new StreamWriter(filename))
            {
                file.Write(result);
            }
            AppendLog($"{filename}�� ��÷ ����� ����Ǿ����ϴ�.", true);
        }

        void calculateACopyLimits() {
            if (!considerSurgicalCopy) return;

            //���� �ο���, �ش� �� ����, a ī�� �������� ���
            //¦���� �ο� �� ���� �Ҵ�
            if (surgicalCopyComposition.Any(t => t.Item1 % 2 == 0))
            {
                Tuple<int, int, int> evenTuple = surgicalCopyComposition.Where(t => t.Item1 % 2 == 0).First();
                surgicalCopyComposition.Add(new Tuple<int, int, int>(evenTuple.Item1, evenTuple.Item2, evenTuple.Item1 / 2));
                surgicalCopyComposition.Remove(evenTuple);
            }

            //Ȧ���� �� �Ҵ�
            if (surgicalCopyComposition.Any(t => t.Item1 % 2 == 1))
            {
                Tuple<int, int, int> oddTuple = surgicalCopyComposition.Where(t => t.Item1 % 2 == 1).First();
                int TotalAPopulation = population.Count(p => p.surgeryCopy.Equals('A'));
                int LesserALimitPerTeam = oddTuple.Item1 / 2;
                int LargerALimitTeamCount = TotalAPopulation - (LesserALimitPerTeam * oddTuple.Item2);
                if (surgicalCopyComposition.Any(t => t.Item1 % 2 == 0))
                {
                    Tuple<int, int, int> evenTuple = surgicalCopyComposition.Where(t => t.Item1 % 2 == 0).First();
                    LargerALimitTeamCount -= evenTuple.Item3 * evenTuple.Item2;
                }
                surgicalCopyComposition.Add(new Tuple<int, int, int>(oddTuple.Item1, LargerALimitTeamCount, LesserALimitPerTeam + 1));
                surgicalCopyComposition.Add(new Tuple<int, int, int>(oddTuple.Item1, oddTuple.Item2 - LargerALimitTeamCount, LesserALimitPerTeam));
                surgicalCopyComposition.Remove(oddTuple);
            }
        }

        void calculateGenderLimits(List<Group> groups)
        {
            int maleCount = population.Where(p => p.gender.Equals('M')).Count();
            int malePerTeamUpperLimit = (int)decimal.Ceiling((maleCount / teamCountNumeric.Value));
            int lowerMaleTeamCount = (int)(malePerTeamUpperLimit * teamCountNumeric.Value - maleCount);

            int malePerTeamLowerLimit = malePerTeamUpperLimit - 1;
            int upperMaleTeamCount = (int)teamCountNumeric.Value - lowerMaleTeamCount;

            int UpperPPTLimit = surgicalCopyComposition.MaxBy(t => t.Item1).Item1;
            var orderdGroups = groups.OrderByDescending(g => g.limit);
            foreach (var item in orderdGroups.Select((value, index) => (value, index)))
            {
                if (item.index < upperMaleTeamCount)
                    item.value.M_limit = malePerTeamUpperLimit;
                else
                    item.value.M_limit = malePerTeamLowerLimit;
            }
        }

        private void AppendLog(string text, bool ignoreOption)
        {
            if (ignoreOption || isLoggingOn)
            {
                logRichTextBox.AppendText($"{text}\n");
                logRichTextBox.ScrollToCaret();
            }
        }

        public void AppendLineWithTimestamp(string text, bool ignoreOption)
        {
            AppendLog($"{DateTime.Now.ToString("O")} : {text}", ignoreOption);
        }

        private List<int> GetTeamTrySequence()
        {
            List<int> seq;
            // ����
            seq = new List<int>();
            for (int i = 0; i < (int)teamCountNumeric.Value; i++)
            {
                seq.Add(i);
            }
            seq = seq.OrderBy(x => Random.Shared.Next()).ToList();
            return seq;
        }

        private void WhenNoSingleModeSettingChanged()
        {
            if (genderRatioMode > 2 && (personPerTeam % 2 != 0 || populationPerTeamLabel2.Visible))
                whenNoSinglePanel.Visible = true;
            else
                whenNoSinglePanel.Visible = false;
        }

        private void Legacy(char minorGender) {
            //�˰��� ¥�� �������� ���� �� �ڵ� ������ �Լ�
            if (genderRatioMode > 2) //�ܵ����� ���� Ȥ�� ���չ����� ��� �Ҽ� �������� ��÷
            {
                var grouping = population.GroupBy(person => person.gender);
                if (minorGender.Equals('M'))
                    grouping = grouping.OrderByDescending(iGrouping => iGrouping.Key);
                else
                    grouping = grouping.OrderBy(iGrouping => iGrouping.Key);

                population = grouping.SelectMany(iGrouping => iGrouping.OrderBy(x => Random.Shared.Next()))
                    .ToList();
            }
            else
            {
                population = population.OrderBy(x => Random.Shared.Next()).ToList();
            }

/*            if (genderRatioMode > 2)
            {
                //���񿡼� �ܵ����� ���� �ʿ��� ���, minorGender ���� �ʿ� �켱������ ����
                seq = groups
                    .Select(g => new
                    {
                        minorGenderCount = g.GetGenderCount(minorGender),
                        groupIndex = g.groupNumber - 1
                    }).GroupBy(t => t.minorGenderCount)
                    .OrderByDescending(t => t.Key)
                    .SelectMany(t => t.OrderBy(x => Random.Shared.Next()))
                    .Select(t => t.groupIndex).ToList();
            }*/
        }
        #endregion
    }
}
