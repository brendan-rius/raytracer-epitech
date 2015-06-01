using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Action> actions = new List<Action>();
            actions.Add(new TriangleAction());
            actions.Add(new TextureAction());

            foreach (var action in actions)
            {
                if (action.Match(s))
                    action.Act();
            }
        }
    }

    abstract class Action
    {
        public abstract bool Match(string s);
        public abstract void Act(Scene scene);
    }

    class TriangleAction : Action
    {
        public override bool Match(string s)
        {
            return true;
        }

        public override void Act()
        {
            Console.WriteLine("triangle");
        }
    }

    class TextureAction : Action
    {
        public override bool Match(string s)
        {
            return true;
        }

        public override void Act(Scene scene)
        {
            Console.WriteLine("texture");
        }
    }
}
