using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace JxcSystem.Views
{
    public class ExcelResult
    {
        /// <summary>
        /// 提供将泛型集合数据导出Excel文档。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class ExcelResults<T> : ActionResult where T : new()
        {
            public ExcelResults(IList<T> entity, string fileName, bool showDisplayName = true)
            {
                this.Entity = entity;
                this.FileName = fileName;
                this.ShowDisplayName = showDisplayName;
            }

            public ExcelResults(IList<T> entity, bool showDisplayName = true)
            {
                this.Entity = entity;

                DateTime time = DateTime.Now;
                this.FileName = string.Format("{0}_{1}_{2}_{3}",
                    time.Month, time.Day, time.Hour, time.Minute);
                this.ShowDisplayName = showDisplayName;
            }

            public IList<T> Entity
            {
                get;
                set;
            }

            public string FileName
            {
                get;
                set;
            }

            public bool ShowDisplayName
            {
                get;
                set;
            }

            public override void ExecuteResult(ControllerContext context)
            {
                if (Entity == null)
                {
                    new EmptyResult().ExecuteResult(context);
                    return;
                }

                SetResponse(context);
            }

            /// <summary>
            /// 设置并向客户端发送请求响应。
            /// </summary>
            /// <param name="context"></param>
            private void SetResponse(ControllerContext context)
            {
                StringBuilder sBuilder = ConvertEntity();
                byte[] bytestr = Encoding.Unicode.GetBytes(sBuilder.ToString());
                context.HttpContext.Response.Clear();
                context.HttpContext.Response.ClearContent();
                context.HttpContext.Response.Buffer = true;
                context.HttpContext.Response.Charset = "GB2312";
                //添加中文GB2312格式 
                context.HttpContext.Response.ContentEncoding = Encoding.GetEncoding("GB2312");
                context.HttpContext.Response.ContentType = "application/ms-excel";
                context.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".xls");
                context.HttpContext.Response.AddHeader("Content-Length", bytestr.Length.ToString());
                context.HttpContext.Response.Write(sBuilder);
                //添加Flush方法        
                context.HttpContext.Response.Flush();
                //添加Close方法           
                context.HttpContext.Response.Close();
                context.HttpContext.Response.End();
            }

            /// <summary>
            /// 把泛型集合转换成组合Excel表格的字符串。
            /// </summary>
            /// <returns></returns>
            private StringBuilder ConvertEntity()
            {
                StringBuilder sb = new StringBuilder();
                AddTableHead(sb);
                AddTableBody(sb);
                return sb;
            }

            /// <summary>
            /// 根据IList泛型集合中的每项的属性值来组合Excel表格。
            /// </summary>
            /// <param name="sb"></param>
            private void AddTableBody(StringBuilder sb)
            {
                if (Entity == null || Entity.Count <= 0)
                {
                    return;
                }

                PropertyInfo[] properties = typeof(T).GetProperties();

                if (properties.Length <= 0)
                {
                    return;
                }

                for (int i = 0; i < Entity.Count; i++)
                {
                    for (int j = 0; j < properties.Length; j++)
                    {
                        string sign = j == properties.Length - 1 ? "\n" : "\t";
                        object obj = properties[j].GetValue(Entity[i], null);
                        sb.Append(obj ?? string.Empty).Append(sign);
                    }
                }
            }

            /// <summary>
            /// 根据指定类型T的所有属性名称来组合Excel表头。
            /// </summary>
            /// <param name="sb"></param>
            private void AddTableHead(StringBuilder sb)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();

                if (properties.Length <= 0)
                {
                    return;
                }

                for (int i = 0; i < properties.Length; i++)
                {
                    string headName = properties[i].Name;
                    string sign = i == properties.Length - 1 ? "\n" : "\t";
                    if (!ShowDisplayName)
                    {
                        sb.Append(headName).Append(sign);
                        continue;
                    }

                    Attribute attribute = Attribute.GetCustomAttribute(properties[i], typeof(DisplayNameAttribute));
                    if (attribute != null)
                    {
                        DisplayNameAttribute displayNameAttribute = attribute as DisplayNameAttribute;
                        if (displayNameAttribute != null && !string.IsNullOrWhiteSpace(displayNameAttribute.DisplayName))
                        {
                            headName = displayNameAttribute.DisplayName;
                        }
                    }

                    sb.Append(headName).Append(sign);
                }
            }
        }
    }
}