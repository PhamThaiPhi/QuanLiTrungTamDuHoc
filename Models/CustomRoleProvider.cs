﻿using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Web;
using System.Web.Security;
using quanlytrungtam.Models;

namespace quanlytrungtam.Models
{

    public class CustomRoleProvider : RoleProvider
    {
        QUANLYTRUNGTAMDUHOCEntities1 db = new QUANLYTRUNGTAMDUHOCEntities1();
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string name)
        {
            // tạo biến getrole, so sánh xem UserID đang đăng nhập có giống với tên trong db ko
            NHANVIEN account = db.NHANVIEN.Single(x => x.HOTEN.Equals(name));
            if (account != null) // Nếu giống
            {
                return new String[] { account.CHUCVU.TENCV };
            }
            else
                return new String[] { };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}