using Logic;

namespace AlphaBank
{
    public class RBank : IRBank
    {
        private readonly UpdateSession _ss = new UpdateSession();

        public bool ReleaseData()
        {
            return _ss.ReleaseData();
        }
    }
}
