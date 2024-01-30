using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace waterfood.Core.Services.Interfaces
{
    public interface IEmailService
    {
        string SendRegisterCodeEmail(string email);
        string SendForgotPasswordCodeEmail(string email);
        string SenEmail(string email, string code, string sub, string message);
    }
}
