using System.Linq.Expressions;
using LinqToQuerystring.Nodes.Base;

namespace LinqToQuerystring.Nodes.DataTypes
{
    internal class StringNode : SingleNode
    {
        public StringNode(string value) : base(value)
        {
        }

        public override Expression CreateExpression()
        {
            return Expression.Constant(Value.Trim('\''));
        }
    }
}
