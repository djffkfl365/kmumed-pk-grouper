using System.Linq.Expressions;
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
        List<Tuple<int, int, int>> surgicalCopyComposition; //조별 인원수, 해당 조 개수, A copy limit로 사용 / !!!!Count 최대 2개!!!!

        public MainForm()
        {
            InitializeComponent();
        }

        private void DoLottery() {
            AppendLineWithTimestamp("추첨을 위해 프로그램 초기화 진행.", true);
            //calculate minor gender
            int maleCount = population.Where(n => n.gender.Equals('M')).Count();
            char minorGender;
            if (maleCount > Math.Floor((decimal)population.Count / 2))
                minorGender = 'F';
            else
                minorGender = 'M';
            AppendLineWithTimestamp($"남성: {maleCount}명 / 여성 {population.Count - maleCount}명", true);
            if(genderRatioMode != 0)
                AppendLog($"소수인 성은 {minorGender} 이며, 성비를 고려하여 추첨합니다", true);

            //initialize Groups: 조 AB카피 등 구성 사전설정
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
                AppendLog($"{t.Item1} 명조 {t.Item2}개가 생성되었습니다.", true);
                if (considerSurgicalCopy)
                    AppendLog($"- A Copy 최대 {t.Item3}명까지 배정됩니다", true);
            }

            if(genderRatioMode == 1)
                calculateGenderLimits(groups);

            groups = groups.OrderByDescending(g => g.limit).ToList();

            AppendLineWithTimestamp($"최종 {groups.Count}개의 조가 생성되었습니다.", true);


            //추출 순서 radomize and initialize
            List<int> seq;
            int cycleCount = 0;
            bool printResult = true;
            AppendLineWithTimestamp($"초기화 완료. 추첨을 시작합니다.", true);
            do
            {
                //cycle마다 필요한 초기화
                cycleCount++;
                printResult = true;
                groups.ForEach(g => g.Reset());
                //cycle 마다 사람 추출순서 randomize
                population = population.OrderBy(x => Random.Shared.Next()).ToList();

                AppendLineWithTimestamp($"{cycleCount}번째 cycle 추첨을 시작합니다.", false);
                int current = 0;
                foreach (Node n in population)
                {
                    current++;
                    //사람마다 group 시도 순서 randomize
                    seq = GetTeamTrySequence();
                    for (int i = 0; i < groups.Count; i++)
                    {
                        if (groups[seq[i]].Add(n))
                        {
                            AppendLineWithTimestamp($"{cycleCount}-{current}; {n.HandleName}가 {seq[i] + 1}조에 배정되었습니다", false);
                            if(isLoggingOn && cycleCount - 1 < loggingUntilCycleNum.Value)
                                Thread.Sleep((int)(loggingDelayNum.Value * 1000));
                            break;
                        }
                        if (i == groups.Count - 1)
                        {
                            AppendLineWithTimestamp("!! 특정인이 모든 조에 대해 배정될 수 없어 다시 추첨해야 합니다!!", false);
                            printResult = false;
                        }
                    }
                    if (!printResult)
                        break; //배정 불가한 case 발생한 경우 종료 후 재추첨
                }
            } while (!printResult && cycleCount < 100);

            if (printResult)
            {
                AppendLineWithTimestamp($"{cycleCount}회 추첨 결과 추첨에 성공했습니다", true);
                StartButton.Enabled = false;
                foreach (Group g in groups)
                {
                    AppendLog(g.Print(true), true);
                }
                SaveCSV(groups);
            }
            else
            {
                AppendLineWithTimestamp($"{cycleCount}회 추첨 결과 추첨에 실패했습니다. 성비 고려 조건을 변경하거나 다시 시도해주세요.", true);
            }
        }

        #region Event handlers
        private void dbFilePath_Click(object sender, EventArgs e)
        {
            string filePath = ShowGeneralFileOpenDialog(
                title: "데이터 파일 선택",
                initialDirectory: "%HOMEPATH%\\Desktop",
                filter: "쉼표로 구분된 값 파일 (*.csv)|*.csv",
                defaultExt: "csv"
                );
            if (Path.Exists(filePath))
            {
                dbFilePathTB.Text = filePath;
            }
            else
            {
                MessageBox.Show("파일이 선택되지 않았거나 존재하지 않습니다. 다시 시도해주세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void totalPopulationNumeric_ValueChanged(object sender, EventArgs e)
        {
            int totalCount = (int)totalPopulationNumeric.Value;
            int teamCount = (int)teamCountNumeric.Value;
            int localPersonPerTeam = (int)Math.Ceiling(totalCount / teamCountNumeric.Value);
            if (totalCount == localPersonPerTeam * teamCount)
            {
                populationPerTeamLabel1.Text = $"{localPersonPerTeam}명조";
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
                populationPerTeamLabel1.Text = $"{localPersonPerTeam - 1}명조";
                populationPerTeamLabel2.Text = $"{localPersonPerTeam}명조";
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
            if (string.IsNullOrEmpty(dbFilePathTB.Text))
                return;

            try
            {
                population = LoadCSV(dbFilePathTB.Text);
            }
            catch (FileNotFoundException ex)
            {
                AppendLineWithTimestamp($"{ex.Message}", true);
            }

            if (population != null && population.Count > 0)
                totalPopulationNumeric.Value = population.Count;
            else
            {
                AppendLineWithTimestamp("제대로 인식된 사람이 없어 추첨을 진행할 수 없습니다. 파일 오류를 확인한 후 다시 시도해주세요.", true);
                dbFilePathTB.Text = "";
                return;
            }

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
                MessageBox.Show("데이터 파일이 선택되지 않았거나 존재하지 않습니다. 다시 시도해주세요.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        /// 일반적 용도로 사용할 수 있는 파일 선택 다이얼로그입니다.
        /// </summary>
        /// <param name="title">대화 상자의 제목 표시줄에 나타낼 문자열입니다.</param>
        /// <param name="initialDirectory">대화 상자의 초기 디렉터리입니다. \로 끝납니다.</param>
        /// <param name="filename">대화 상자에 처음 표시된 파일 또는 사용자가 선택한 파일 이름입니다.</param>
        /// <param name="defaultExt">기본 파일 이름 확장명입니다. 사용자가 확장명을 입력하지 않고 파일을 선택하면 끝에 이 확장명이 추가됩니다.</param>
        /// <param name="filter">대화 상자에 표시할 파일 필터입니다(예 : "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*")</param>
        /// <param name="filterIndex">대화 상자에서 선택한 파일 필터의 인덱스입니다. 첫 번째 인덱스는 1입니다.</param>
        /// <returns>파일의 절대 경로(예 : "C:\Users\filename.ext")를 반환합니다.</returns>
        public static string ShowGeneralFileOpenDialog(string title = "파일을 선택해주세요", string initialDirectory = "MyComputer", string? filename = null, string? defaultExt = null, string filter = "모든 파일(*.*)|*.*", int filterIndex = 1)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = title;
            openFileDialog.InitialDirectory = initialDirectory;
            openFileDialog.FileName = filename;
            openFileDialog.Filter = filter;
            openFileDialog.DefaultExt = defaultExt;
            openFileDialog.FilterIndex = filterIndex;
            DialogResult dr = openFileDialog.ShowDialog();
            //OK버튼 클릭시
            if (dr == DialogResult.OK)
            {
                //File경로와 File명을 모두 반환
                return openFileDialog.FileName;
            }
            return null;
        }

        List<Node> LoadCSV(string fileName)
        {
            string[] buff;
            if (File.Exists(fileName))
            {
                AppendLog($"{fileName}을 로드합니다", true);
                buff = File.ReadAllLines(fileName, Encoding.UTF8);
            }
            else
                throw new FileNotFoundException($"파일명 '{fileName}'이 존재하지 않습니다.");

            if (buff[0].Split(',')[0].Equals("�й�"))
            {
                int euckrCodePage = 51949;  // euc-kr 코드 번호
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding euckr = Encoding.GetEncoding(euckrCodePage);

                buff = File.ReadAllLines(fileName, Encoding.GetEncoding("euc-kr"));
            }

            List<Node> table = new List<Node>();
            foreach (var d in buff.Select((value, index) => (value, index)))
            {
                try
                {
                    string[] split = d.value.Split(',');
                    if (split[0].Equals("학번")) continue; //첫 행 무시
                    table.Add(new Node(split, d.index));
                }
                catch (ArgumentException ex)
                {
                    AppendLog($"{ex.Message}", true);
                }
            }

            table.GroupBy(i => i.Name)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key) // 중복되는 이름 찾아서
                .ToList()
                .ForEach(s => 
                    {
                        table.FindAll(n => n.Name.Equals(s)) //Instance안에서 학번 병기하도록 변경
                            .ForEach(n => n.Name = $"{n.Name}({n.Id})");
                    }
                );
            return table;
        }

        private void SaveCSV(List<Group> groups)
        {
            AppendLineWithTimestamp($"추첨 결과를 저장합니다.", true);
            //Header
            int maxPPT = groups.Max(g => g.limit);
            string result = "조,남,여,";
            string[] headerPersons = new string[maxPPT];
            for (int i = 0; i < maxPPT; i++)
                headerPersons[i] = $"조원{i + 1}";
            result += $"{string.Join(",", headerPersons)}\n";

            //Body
            groups.OrderBy(g => g.groupNumber);
            groups.ForEach(g => result += $"{g.PrintForCSV()}\n");

            //Save
            string filename = $"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\\실습조 추첨 결과 - {DateTime.Now.ToString("yyyyMMdd hhmmss")}.csv";
            using (StreamWriter file = new StreamWriter(filename))
            {
                file.Write(result);
            }
            AppendLog($"{filename}에 추첨 결과가 저장되었습니다.", true);
        }

        void calculateACopyLimits() {
            if (!considerSurgicalCopy) return;

            //조당 인원수, 해당 조 개수, a 카피 리밋으로 사용
            //짝수인 인원 조 먼저 할당
            if (surgicalCopyComposition.Any(t => t.Item1 % 2 == 0))
            {
                Tuple<int, int, int> evenTuple = surgicalCopyComposition.Where(t => t.Item1 % 2 == 0).First();
                surgicalCopyComposition.Add(new Tuple<int, int, int>(evenTuple.Item1, evenTuple.Item2, evenTuple.Item1 / 2));
                surgicalCopyComposition.Remove(evenTuple);
            }

            //홀수인 조 할당
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
            // 랜덤
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
            //알고리즘 짜는 과정에서 나온 구 코드 보존용 함수
            if (genderRatioMode > 2) //단독구성 배제 혹은 복합배제인 경우 소수 성별부터 추첨
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
                //성비에서 단독구성 배제 필요한 경우, minorGender 많은 쪽에 우선순위로 배정
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
