using System.Collections.Generic;

namespace Befunge
{
    public static class LongStackExtenstions
    {
        public static long PopOrZero(this Stack<long> stack)
        {
            long top = 0;
            stack.TryPop(out top);
            return top;
        }

        public static long PeekOrZero(this Stack<long> stack)
        {
            long top = 0;
            stack.TryPeek(out top);
            return top;
        }
    }
}

