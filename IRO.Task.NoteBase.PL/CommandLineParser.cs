using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace IRO.Task.NoteBase.PL
{
    static class CommandLineParser
    {
        static public string[] Parse(string lineToParse)
        {
            if (String.IsNullOrWhiteSpace(lineToParse))
                return new string[1];
            Regex regex = new Regex("(\"[^\"]+\"|[^\\s\"]+)");
            MatchCollection args = regex.Matches(lineToParse);
            var arguments = new List<string>();
            foreach(Match argument in args)
            {
                arguments.Add(argument.Value.Replace("\"", String.Empty));
            }
            return arguments.ToArray();
        }
    }
}
