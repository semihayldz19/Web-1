using Course.InAndOutModels;
using Course.Interfaces;
using Course.InAndOutModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Interfaces
{
    public interface IUser : IBase2<MUser.Response, MUser.Form, MUser.FilterForm>
    {

        Task<MethodResponse<MUser.Response>> Login(MUser.LoginForm form, string currentUserId);



        //object Login(MUser.Form form, string currentUserId);
        //object Login(MUser.LoginForm form, string currentUserId);

        //Task<Method> // eksik tamamla
    }
}