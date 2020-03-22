using System.Linq.Expressions;
using LinqToQuerystring.Nodes.Base;

namespace LinqToQuerystring.Nodes.DataTypes
{
    internal class NullNode : BaseNode
    {
        public override Expression CreateExpression()
        {
            return Expression.Constant(null);
        }
    }
}
