#pragma checksum "D:\coding\VisualStudioProj\EDP\Elderson\Pages\Users\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e42c1a39b2262ea340549d27aaa24eebc9a3e6a0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(Elderson.Pages.Users.Pages_Users_Index), @"mvc.1.0.razor-page", @"/Pages/Users/Index.cshtml")]
namespace Elderson.Pages.Users
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\coding\VisualStudioProj\EDP\Elderson\Pages\_ViewImports.cshtml"
using Elderson;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e42c1a39b2262ea340549d27aaa24eebc9a3e6a0", @"/Pages/Users/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d8adfb8bf041aa97388a84a14fee59c48bc6b006", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Users_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "D:\coding\VisualStudioProj\EDP\Elderson\Pages\Users\Index.cshtml"
  
    Layout = "Shared/_LayoutITSupport";
    ViewData["Title"] = "User management page";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""card"">
    <div class=""card-body"">
        <h4 class=""card-title"">Hoverable Table</h4>
        <p class=""card-description"">
            Add class <code>.table-hover</code>
        </p>
        <div class=""table-responsive"">
            <table class=""table table-hover"">
                <thead>
                    <tr>
                        <th>User</th>
                        <th>Type</th>
                        <th>Status</th>
                        <th>Action</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Jacob</td>
                        <td>Patient</td>
                        <td class=""text-danger""><label class=""badge badge-danger"">Locked</label></td>
                        <td>
                            <div class=""btn-group"">
                                <button type=""button"" class=""btn btn-primary"">View</button>
                                <button type=""button"" cl");
            WriteLiteral(@"ass=""btn btn-primary dropdown-toggle dropdown-toggle-split"" id=""dropdownMenuSplitButton1"" data-bs-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                                </button>
                                <div class=""dropdown-menu"" aria-labelledby=""dropdownMenuSplitButton1"">
                                    <a class=""dropdown-item"" href=""#"">Edit</a>
                                    <a class=""dropdown-item"" href=""#"">Lock</a>
                                    <a class=""dropdown-item"" href=""#"">Change password</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Jacob</td>
                        <td>Patient</td>
                        <td><label class=""badge badge-warning"">Pending Verification</label></td>
                        <td class=""text-danger"">
                            <div class=""btn-group"">
                      ");
            WriteLiteral(@"          <button type=""button"" class=""btn btn-primary"">View</button>
                                <button type=""button"" class=""btn btn-primary dropdown-toggle dropdown-toggle-split"" id=""dropdownMenuSplitButton1"" data-bs-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                                </button>
                                <div class=""dropdown-menu"" aria-labelledby=""dropdownMenuSplitButton1""");
            BeginWriteAttribute("style", " style=\"", 2633, "\"", 2641, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                    <a class=""dropdown-item"" href=""#"">Edit</a>
                                    <a class=""dropdown-item"" href=""#"">Lock</a>
                                    <a class=""dropdown-item"" href=""#"">Change password</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Jacob</td>
                        <td>Patient</td>
                        <td><label class=""badge badge-danger"">Locked</label></td>
                        <td class=""text-danger"">
                            <div class=""btn-group"">
                                <button type=""button"" class=""btn btn-primary"">View</button>
                                <button type=""button"" class=""btn btn-primary dropdown-toggle dropdown-toggle-split"" id=""dropdownMenuSplitButton1"" data-bs-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                                ");
            WriteLiteral("</button>\r\n                                <div class=\"dropdown-menu\" aria-labelledby=\"dropdownMenuSplitButton1\"");
            BeginWriteAttribute("style", " style=\"", 3778, "\"", 3786, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                    <a class=""dropdown-item"" href=""#"">Edit</a>
                                    <a class=""dropdown-item"" href=""#"">Lock</a>
                                    <a class=""dropdown-item"" href=""#"">Change password</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Jacob</td>
                        <td>Patient</td>
                        <td><label class=""badge badge-danger"">Locked</label></td>
                        <td class=""text-danger"">
                            <div class=""btn-group"">
                                <button type=""button"" class=""btn btn-primary"">View</button>
                                <button type=""button"" class=""btn btn-primary dropdown-toggle dropdown-toggle-split"" id=""dropdownMenuSplitButton1"" data-bs-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                                ");
            WriteLiteral("</button>\r\n                                <div class=\"dropdown-menu\" aria-labelledby=\"dropdownMenuSplitButton1\"");
            BeginWriteAttribute("style", " style=\"", 4923, "\"", 4931, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                    <a class=""dropdown-item"" href=""#"">Edit</a>
                                    <a class=""dropdown-item"" href=""#"">Lock</a>
                                    <a class=""dropdown-item"" href=""#"">Change password</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Jacob</td>
                        <td>Patient</td>
                        <td><label class=""badge badge-danger"">Locked</label></td>
                        <td class=""text-danger"">
                            <div class=""btn-group"">
                                <button type=""button"" class=""btn btn-primary"">View</button>
                                <button type=""button"" class=""btn btn-primary dropdown-toggle dropdown-toggle-split"" id=""dropdownMenuSplitButton1"" data-bs-toggle=""dropdown"" aria-haspopup=""true"" aria-expanded=""false"">
                                ");
            WriteLiteral("</button>\r\n                                <div class=\"dropdown-menu\" aria-labelledby=\"dropdownMenuSplitButton1\"");
            BeginWriteAttribute("style", " style=\"", 6068, "\"", 6076, 0);
            EndWriteAttribute();
            WriteLiteral(@">
                                    <a class=""dropdown-item"" href=""#"">Edit</a>
                                    <a class=""dropdown-item"" href=""#"">Lock</a>
                                    <a class=""dropdown-item"" href=""#"">Change password</a>
                                </div>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Elderson.Pages.Users.UserManagementModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Elderson.Pages.Users.UserManagementModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<Elderson.Pages.Users.UserManagementModel>)PageContext?.ViewData;
        public Elderson.Pages.Users.UserManagementModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591