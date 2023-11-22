namespace kmumed_pk_grouper
{
    internal class Node
    {
        public readonly int Id;
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public readonly char gender;
        public readonly string ban;
        public readonly char surgeryCopy;
        public readonly string HandleName;

        public Node(string[] split)
        {
            if (split == null) throw new ArgumentNullException("데이터가 비어있습니다.");
            if (split.Length < 6) throw new ArgumentOutOfRangeException("데이터 파일의 행이 6개 미만입니다.");
            if (string.IsNullOrWhiteSpace(split[0])) throw new ArgumentNullException("학번이 비어 있습니다.");
            Id = int.Parse(split[0].Trim());
            if (string.IsNullOrWhiteSpace(split[1])) throw new ArgumentNullException($"학번 {Id}의 이름이 비어 있습니다.");
            Name = split[1].Trim();
            if (string.IsNullOrWhiteSpace(split[2])) throw new ArgumentNullException($"{Name}의 별명이 비어 있습니다.");
            HandleName = split[2].Trim();

            string genderTemp = split[4].Trim().ToUpper();
            if (!string.IsNullOrWhiteSpace(genderTemp))
            {
                gender = char.Parse(genderTemp);
            }
            if (!string.IsNullOrEmpty(genderTemp) && !(gender.Equals('M') || gender.Equals('F')))
            {
                throw new ArgumentOutOfRangeException($"{HandleName}의 성별이 비어 있지 않으면서 M 또는 F가 아닙니다.");
            }

            if (!string.IsNullOrEmpty(split[5])) {
                surgeryCopy = char.Parse(split[5].Trim().ToUpper());
                if (!(surgeryCopy.Equals('A') || surgeryCopy.Equals('B')))
                {
                    throw new ArgumentOutOfRangeException($"{HandleName}의 외과 Copy가 비어 있지 않으면서 A 또는 B가 아닙니다.");
                }
            }
            if (!string.IsNullOrEmpty(split[3]))
                ban = split[3].Trim();
        }

    }
}
