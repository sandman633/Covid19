﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Covid19.Infrastructure.Commands.Base;

namespace Covid19.Infrastructure.Commands
{
    internal class LambdaCommand : Command
    {
        private readonly Action<object> _execute;
        private readonly Func<object?, bool> _canExecute;

        public LambdaCommand(Action<object> execute, Func<object?,bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(Execute));
            _canExecute = canExecute;
        }
        public override bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter)??true;

        public override void Execute(object? parameter) => _execute(parameter);
    }
}
