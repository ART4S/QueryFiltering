using System.Linq.Expressions;
using LinqToQuerystring.Nodes.Base;

namespace LinqToQuerystring.Nodes.Operators
{
    internal class EqualsNode : OperationNode
    {
        public EqualsNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        protected override Expression CreateExpressionImpl(Expression left, Expression right)
        {
            return Expression.Equal(left, right);
        }
    }
}
