using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DocoptNet
{
    /// <summary>
    ///     Branch/inner node of a pattern tree.
    /// </summary>
    internal class BranchPattern : Pattern
    {
        public BranchPattern(params Pattern[] children) 
            => Children = children ?? throw new ArgumentNullException(nameof(children));

        public override bool HasChildren => true;

        public IEnumerable<Pattern> Flat<T>() where T: Pattern 
            => Flat(typeof (T));

        public override ICollection<Pattern> Flat(params Type[] types)
        {
            if (types == null) 
                throw new ArgumentNullException(nameof(types));

            if (types.Contains(GetType()))
                return new Pattern[] {this};
            
            return Children.SelectMany(child => child.Flat(types)).ToList();
        }

        public override string ToString() 
            => $"{GetType().Name}({string.Join(", ", Children.Select(c => c?.ToString() ?? "None"))})";
    }
}