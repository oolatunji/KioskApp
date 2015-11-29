using KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskSolutionLibrary.DataLibrary
{
    public class BranchDL
    {
        public BranchDL()
        {

        }

        public static bool Save(Branch branch)
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    context.Branches.Add(branch);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool BranchExists(Branch branch)
        {
            try
            {
                var existingBranch = new Branch();
                using (var context = new KioskWebDBEntities())
                {
                    existingBranch = context.Branches
                                    .Include("Users")
                                    .Where(t => t.Name.Equals(branch.Name) || t.Code.Equals(branch.Code))
                                    .FirstOrDefault();
                }

                if (existingBranch == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Branch> RetrieveBranches()
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    var branches = context.Branches
                                    .Include("Users")
                                    .ToList();

                    return branches;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Branch RetrieveBranchByID(long? branchID)
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    var branches = context.Branches
                                            .Include("Users")
                                            .Where(f => f.ID == branchID);

                    return branches.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Update(Branch branch)
        {
            try
            {
                Branch existingBranch = new Branch();
                using (var context = new KioskWebDBEntities())
                {
                    existingBranch = context.Branches
                                    .Include("Users")
                                    .Where(t => t.ID == branch.ID)
                                    .FirstOrDefault();
                }

                if (existingBranch != null)
                {
                    existingBranch.Name = branch.Name;
                    existingBranch.Code = branch.Code;
                    existingBranch.Address = branch.Address;

                    using (var context = new KioskWebDBEntities())
                    {
                        context.Entry(existingBranch).State = EntityState.Modified;

                        context.SaveChanges();
                    }

                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
