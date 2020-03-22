using System.Linq.Expressions;

namespace LinqToQuerystring.Nodes.Base
{
    internal abstract class BaseNode
    {
        public abstract Expression CreateExpression();
    }
}
