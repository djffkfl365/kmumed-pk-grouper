namespace kmumed_pk_grouper
{
    internal class Group
    {
        private static int CumulativeGroupCount = 0;
        public readonly int groupNumber;
        public List<Node> members = new List<Node>();
        private char? minorGender;
        public readonly int limit = 4; //기본값 = 4인조
        private int? A_limit = 0;
        public int M_limit = 0;

        #region constructors
        public Group()
        {
            A_limit = 0;
            groupNumber = CumulativeGroupCount + 1;
            CumulativeGroupCount++;
        }
        /// <summary>
        /// Group class의 기본 Constructor 입니다.
        /// </summary>
        /// <param name="limit">해당 조의 인원수를 입력합니다.</param>
        /// <param name="minorGender">성비 고려 모드가 0이 아닌 경우 더 많은 쪽의 성별을 입력합니다. M 또는 F가 가능합니다.</param>
        /// <param name="a_limit">외과 Copy를 고려하는 경우 A 카피의 최대 인원수를 입력합니다.</param>
        /// <exception cref="ArgumentNullException">경우에 따라 필수적인 변수가 입력되지 않은 경우 발생합니다.</exception>
        public Group(int limit, char? minorGender, int? a_limit)
        {
            if (MainForm.considerSurgicalCopy && a_limit == null)
            {
                throw new ArgumentNullException("외과 Copy를 고려하는 경우 a_limit 입력이 필요합니다.");
            }
            if (a_limit > limit)
            {
                throw new ArgumentOutOfRangeException($"a_limit은 limit 보다 클 수 없습니다.");
            }
            this.limit = limit;
            this.minorGender = minorGender;
            A_limit = a_limit;
            groupNumber = CumulativeGroupCount + 1;
            CumulativeGroupCount++;
        }

        public static void ResetCumulative()
        {
            CumulativeGroupCount = 0;
        }
        #endregion

        #region simple utility
        public string Print(bool handle = true) {
            var result = $"{limit}명, 남:{MaleCount()}/여:{FemaleCount()}, A:{ACopyCount()}/B:{BCopyCount()} | ";
            if (handle)
                result += $"{string.Join(", ", GetHandles())}";
            else
                result += $"{string.Join(", ", GetNames())}";
            return result;
        }

        public string PrintForCSV()
        {
            return $"{groupNumber},{MaleCount()},{FemaleCount()},{string.Join(",", GetNames())}";
        }

        public int ACopyCount()
        {
            return members.Where(m => m.surgeryCopy.Equals('A')).Count();
        }

        public int BCopyCount()
        {
            return members.Where(m => m.surgeryCopy.Equals('B')).Count();
        }

        public List<string> GetBans() {
            return members.Select(m => m.ban).ToList();
        }

        public List<string> GetNames()
        {
            return members.Select(m => m.Name).OrderBy(s => s).ToList();
        }

        public List<string> GetHandles()
        {
            return members.Select(m => m.HandleName).ToList();
        }

        public void Reset()
        {
            members.Clear();
        }

        private int MaleCount()
        {
            return members.Where(m => m.gender.Equals('M')).Count();
        }

        private int FemaleCount()
        {
            return members.Where(m => m.gender.Equals('F')).Count();
        }

        public int GetGenderCount(char? gender)
        {
            if (gender == null)
                throw new ArgumentNullException("GetGenderCount의 gender parameter가 null인 상태로 호출되었습니다.");
            if (gender.Equals('F'))
                return FemaleCount();
            if (gender.Equals('M'))
                return MaleCount();
            throw new ArgumentOutOfRangeException($"GetGenderCount 에서 사용한 Gender 값이 M 또는 F 에 해당되지 않습니다. 현재 값은 {gender} 입니다.");
        }
        #endregion

        #region Add functions
        public bool Add(Node node) {
            //조별 인원수
            if (members.Count >= limit)
                return false;

            //성비
            switch (MainForm.genderRatioMode)
            {
                case 0: //랜덤배정
                    break; //고려 없이 넘어가면 됨
                case 1: //균등배분
                    switch (node.gender.ToString())
                    {
                        case "M":
                            if (MaleCount() >= M_limit)
                                return false;
                            break;
                        case "F":
                            if (FemaleCount() >= limit - M_limit)
                                return false;
                            break;
                        default:
                            return false;
                    }
                    break;
                case 2: //과반배제
                    if (NoMoreThanHalf(node))
                    {
                        break;
                    }
                    else
                    {
                        return false;
                    }
                case 3: //단독배제
                    if (NeedSameGenderFriend(node))
                    {
                        break;
                    }
                    else {
                        return false;
                    }
                case 4: //과반 및 단독배제
                    if (NoMoreThanHalf(node) && NeedSameGenderFriend(node))
                        break;
                    else { return false; }
            }

            //외과 카피
            if (MainForm.considerSurgicalCopy) {
                switch (node.surgeryCopy.ToString())
                {
                    case "A":
                        if (ACopyCount() >= A_limit)
                            return false;
                        break;
                    case "B":
                        if (BCopyCount() >= limit - A_limit)
                            return false;
                        break;
                    default:
                        return false;
                }
            }

            //밴카드
            if (GetBans().Count > 0 && GetBans().Any(b => { if (b == null) return false; return b.Equals(node.Name); }))
                return false; //기존 멤버들 밴카드에 걸리는지
            if(GetNames().Count > 0 && GetNames().Any(i => i.Equals(node.ban)))
                return false; //신규 밴카드가 기존 멤버 거르는지
            
            //모든 조건에 합치한다면 추가 후 true 반환
            members.Add(node);
            return true;
        }

        private bool NoMoreThanHalf(Node node)
        {
            int minorGenderLimit = limit / 2;
            int minorGenderCount = GetGenderCount(minorGender);
            if (node.gender.Equals(minorGender))
            {
                //추가하고자 하는 노드가 소수인 성인 경우 자리가 있으면 추가
                if (minorGenderLimit - minorGenderCount > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                // 다수 성인 경우 통과. 그냥 추가하면 됨
                return true; ;
            }
        }

        private bool NeedSameGenderFriend(Node node)
        {
            // mode 3,4 는 Add 함수에서 판별하여 이 함수 호출된 상태
            if (limit % 2 != 0)
            {
                if (MainForm.whenNoMoreThanHalf_RandomForOddsTeam)
                {
                    // limit이 홀수면서 랜덤으로 처리하는 경우 그냥 패스
                    return true;
                }
                else if(MainForm.genderRatioMode == 3) //복합배제의 경우 이미 과반은 배제됨
                {
                    // limit이 홀수면서 홀수조에 대해 과반 배제하는 경우
                    return NoMoreThanHalf(node);
                }
            }
            // limit이 짝수인 경우이면서
            else if (limit - members.Count == 1)
            {
                // 한 자리 남은 경우에서(0자리인 경우 위에서 배제됨)
                if ((MaleCount() == 1 || FemaleCount() == 0) && node.gender.Equals('F'))
                {
                    return false;
                }
                else if ((FemaleCount() == 1 || MaleCount() == 0) && node.gender.Equals('M'))
                {
                    return false;
                }
            }
            // 두 자리 이상 남은경우 그냥 추가
            return true;
        }
        #endregion
    }
}
