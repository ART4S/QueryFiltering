using System.Linq.Expressions;
using LinqToQuerystring.Nodes.Base;

namespace LinqToQuerystring.Nodes.Operators
{
    internal class LessThanOrEqualNode : OperationNode
    {
        public LessThanOrEqualNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        protected override Expression CreateExpressionImpl(Expression left, Expression right)
        {
            return Expression.LessThanOrEqual(left, right);
        }
    }
}
