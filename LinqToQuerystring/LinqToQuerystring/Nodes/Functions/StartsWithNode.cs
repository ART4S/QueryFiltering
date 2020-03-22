using LinqToQuerystring.Helpers;
using LinqToQuerystring.Nodes.Base;

namespace LinqToQuerystring.Nodes.Functions
{
    internal class StartsWithNode : FunctionNode
    {
        public StartsWithNode(BaseNode[] parameters) : base(TypeCashe.String.StartsWith, parameters)
        {
        }
    }
}
