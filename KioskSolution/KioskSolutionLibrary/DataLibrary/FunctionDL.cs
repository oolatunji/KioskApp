﻿using KioskSolutionLibrary.ModelLibrary.EntityFrameworkLibrary;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KioskSolutionLibrary.DataLibrary
{
    public class FunctionDL
    {
        public FunctionDL()
        {

        }

        public static bool Save(Function function)
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    context.Functions.Add(function);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool FunctionExists(Function function)
        {
            try
            {
                var existingFunction = new Function();
                using (var context = new KioskWebDBEntities())
                {
                    existingFunction = context.Functions
                                    .Where(t => t.Name.Equals(function.Name))
                                    .FirstOrDefault();
                }

                if (existingFunction == null)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Function> RetrieveFunctions()
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    var functions = context.Functions.ToList();

                    return functions;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Function RetrieveFunctionByID(long functionID)
        {
            try
            {
                using (var context = new KioskWebDBEntities())
                {
                    var function = context.Functions
                                            .Where(f => f.ID == functionID);

                    return function.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool Update(Function function)
        {
            try
            {
                Function existingfunction = new Function();
                using (var context = new KioskWebDBEntities())
                {
                    existingfunction = context.Functions
                                    .Where(t => t.ID == function.ID)
                                    .FirstOrDefault();
                }

                if (existingfunction != null)
                {
                    existingfunction.Name = function.Name;
                    existingfunction.PageLink = function.PageLink;

                    using (var context = new KioskWebDBEntities())
                    {
                        context.Entry(existingfunction).State = EntityState.Modified;

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
