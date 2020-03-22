using System.Linq.Expressions;

namespace LinqToQuerystring.Nodes.Base
{
    internal abstract class OperationNode : AggregateNode
    {
        protected OperationNode(BaseNode left, BaseNode right) : base(left, right)
        {
        }

        public override Expression CreateExpression()
        {
            Expression left = Left.CreateExpression();
            Expression right = Right.CreateExpression();

            if (left.Type != right.Type)
            {
                right = Expression.Convert(right, left.Type);
            }

            return CreateExpressionImpl(left, right);
        }

        protected abstract Expression CreateExpressionImpl(Expression left, Expression right);
    }
}
