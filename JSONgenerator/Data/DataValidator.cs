using JSONgenerator.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JSONgenerator.Data
{
    public class DataValidator
    {
        private Regex _sources;
        private Regex _targets;
        private Regex _method;
        private Regex _timing;
        private Regex _count;
        private Regex _size;
        //private int _numberOfMehthods;

        public DataValidator()
        {
            _sources = new Regex(@"^[A-Z]:\\(.*)?(\\(.*))?$", RegexOptions.IgnoreCase);
            _targets = new Regex(@"^[A-Z]:\\(.*)?(\\(.*))?$", RegexOptions.IgnoreCase);
            _method = new Regex(@"^(full|incremental|differential)$", RegexOptions.IgnoreCase);
            _timing = new Regex(@"^(\*|[0-5]?\d)\s+(\*|[01]?\d|2[0-3])\s+(\*|[1-9]|[12]\d|3[01])\s+(\*|[1-9]|1[0-2])\s+(\*|[0-6])$");
            _count = new Regex(@"^[1-9][0-9]?$");
            _size = new Regex(@"^[1-9][0-9]?$");
        }
        public bool CheckData(ConfigParameters parameters)
        {
            if (CheckNulls(parameters))
                return false;
            if (parameters.Sources.Any() && parameters.Sources.All(s => _sources.IsMatch(s)) &&
                parameters.Sources.Any() && parameters.Sources.All(t => _targets.IsMatch(t)) &&
                _method.IsMatch(parameters.Method) &&
                _timing.IsMatch(parameters.Timing) &&
                _count.IsMatch(parameters.retention.Count.ToString()) &&
                _size.IsMatch(parameters.retention.Size.ToString())
                )
                return true;
            return false;
        }
        private bool CheckNulls(ConfigParameters parameters)
        {
            if (parameters.Name == null ||
                parameters.Sources == null ||
                parameters.Targets == null ||
                parameters.Method == null ||
                parameters.Timing == null ||
                parameters.Output == null)
                return true;
            return false;
        }
    }
}
