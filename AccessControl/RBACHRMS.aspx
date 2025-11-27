<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage/MainMaster.master" EnableEventValidation="false" CodeBehind="RBACHRMS.aspx.cs" Inherits="SystemAdmin.AccessControl.RBACHRMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="portlet box green margin-top-10">
        <div class="portlet-title">
            <div class="caption">
                <asp:Label ID="lblPageListTitle" runat="server" Text="HRMS Access Control"></asp:Label>
            </div>
        </div> 
        <div id="divView" runat="server" class="portlet-body form-body">
            <div class="row">
                <div class="col-md-2 pull-right text-right">
                    <label class="control-label"><span class="required" aria-required="true"></span></label>
                    <div>
                        <div class="btn-group pull-right" style="margin-left: 8px;">  
                            <button class="btn dropdown-toggle" data-toggle="dropdown">
                                Action <i class="fa fa-angle-down"></i>
                            </button>
                            <ul class="dropdown-menu pull-right"> 
                                <li>
                                    <asp:LinkButton ID="lnkBtnAddNew" OnClick="lnkBtnAddNew_Click" runat="server"><i class="fa fa-plus"></i>Add</asp:LinkButton>
                                </li>
                                <li>
                                    <asp:LinkButton ID="lnkBtnEdit" runat="server" OnClick="lnkBtnEdit_Click" OnClientClick="return CheckOnlyOneSelect('checkboxes');" Text="Edit"><i class="fa fa-pencil"></i>Edit</asp:LinkButton>
                                </li> 
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-md-12">
                    <asp:ListView ID="LV_Employee_Access" runat="server" ItemPlaceholderID="itemplaceholder">
                        <LayoutTemplate>
                            <table class="table table-bordered table-hover mydatatable">
                                <thead>
                                    <tr>
                                        <th>#</th>
                                        <th>Employee Name</th>
                                    </tr>
                                </thead>
                                <tr id="itemplaceholder" runat="server" />
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkSelect" class="checkboxes" runat="server" Autoid='<%# Eval("Autoid")%>' />
                                </td>
                                <td>
                                    <%# Eval("EmpName") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
        <div id="divEdit" runat="server" class="portlet-body form" visible="false">
            <div class="form-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label class="control-label">Name<span class="required" aria-required="true"> *</span></label>
                            <asp:DropDownList ID="ddlEmployeeName" OnSelectedIndexChanged="ddlEmployeeName_SelectedIndexChanged" AutoPostBack="true" class="form-control req select2ddl" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="divEmployeeDetails">
                    <div class="col-md-6">
                        <asp:ListView ID="LV_Employee_Menu_Details" runat="server" ItemPlaceholderID="itemplaceholder1">
                            <LayoutTemplate>
                                <table class="table table-bordered">
                                    <thead>
                                        <tr style="background: #ddd;">
                                            <th>Department</th>
                                            <th>Sub Department</th>
                                            <th>Designation</th>
                                        </tr>
                                    </thead>
                                    <tr id="itemplaceholder1" runat="server" />
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("DepartmentName") %>
                                    </td>
                                    <td>
                                        <%# Eval("SubDepartmentName") %>
                                    </td>
                                    <td>
                                        <%# Eval("DesignationName") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <hr />
                    </div>
                </div>
                <div class="row mb-3">
                    
                </div>
 

                <div class="row" runat="server" id="divEmployeeAccess">
                    <div class="col-md-12 ">
                        <div class="form-group pull-right"> 
                            <asp:Button ID="btnApplyAllAccess" runat="server" 
                                CssClass="btn-success btn-xs btn-access" OnClientClick="OpenPopUpAction(); return false;"
                                Text="&#x2714; Apply Bulk Access"   />

                            <asp:Button ID="btnRevokeAllAccess" runat="server" 
                                CssClass="btn-danger btn-xs btn-access" OnClientClick="return confirm('Are you sure you want to Clear all access for this employee? ');" 
                                Text="&#x2716; Clear Access" OnClick="btnRevokeAllAccess_Click"  />
                        </div>
                       </div>
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="upnl_Menuaccess" runat="server" UpdateMode="Conditional">
                            <ContentTemplate> 
                                <asp:ListView ID="LV_Access_Menu_Company" runat="server" ItemPlaceholderID="itemplaceholder2"  GroupPlaceholderID="groupPlaceholder" OnItemDataBound="LV_Hid_Region_Industry_ItemDataBound">
                                    <LayoutTemplate>
                                        <table class="table table-bordered table-hover">
                                            <thead>
                                                <tr style="background:#cdcdcd;"> 
                                                      <th>  Parent</th>
                                                    <th>Sub Parent</th>
                                                    <th>Child</th> 
                                                    <th>Group</th>
                                                    <th>Industry</th>
                                                    <th>Region</th> 
                                                    <th>Organization</th> 
                                                    <th>Company</th>
                                                    <th>Reporting To</th>  
                                                    <th>Location</th> 
                                                    <th>Action</th>
                                                </tr>
                                            </thead> 
                                          <asp:PlaceHolder ID="itemplaceholder2" runat="server" />
                                        </table>
                                    </LayoutTemplate>  
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:HiddenField ID="hidautoid" runat="server" Value='<%# Eval("Autoid")%>' />
                                                <asp:HiddenField ID="hidIsParentMenu" runat="server" Value='<%# Eval("IsMasterMenu")%>' />
                                                <%# Eval("ParentMenuName") %>
                                            </td>
                                            <td>
                                                <%# Eval("SubParentMenuName") %>
                                            </td>
                                            <td>
                                                <%# Eval("MenuName") %>
                                            </td>  
                                            <td>
                                                <asp:Panel runat="server" ID="PnlGroup" CssClass="dropdown-container">
                                                    <button type="button" class="dropdown-button">Choose an item</button> 
                                                        <div class="dropdown-menu">
                                                             <input type="text" class="dropdown-search" placeholder="Search..." /> 
                                                              <asp:CheckBox ID="chkSelectAllGroup" ForeColor="Blue" Font-Bold="true"
                                                              runat="server" Text="Select All"  
                                                              AutoPostBack="true" OnCheckedChanged="chkSelectAllGroup_CheckedChanged" />  
                                                        <div class="company-items-wrapper">
                                                            <asp:CheckBoxList ID="chkactionGroup"
                                                                CssClass="group-checkboxlist" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' AutoPostBack="true"  OnSelectedIndexChanged="chkactionGroup_SelectedIndexChanged"
                                                                runat="server" RepeatDirection="Vertical"
                                                                DataTextField="Name" DataValueField="Id" DataSource='<%# GetGroupByIndustry() %>'  />
                                                        </div>
                                                       </div> 
                                               </asp:Panel> 
                                            </td>
                                             <td>
                                             <asp:Panel runat="server" ID="pnlIndustry" CssClass="dropdown-container">
                                             <button type="button" class="dropdown-button">Choose an item</button> 
                                                     <div class="dropdown-menu">
                                                          <input type="text" class="dropdown-search" placeholder="Search..." /> 
                                                           <asp:CheckBox ID="chkSelectAllIndustry" ForeColor="Blue" Font-Bold="true"
                                                           runat="server" Text="Select All"  
                                                           AutoPostBack="true" OnCheckedChanged="chkSelectAllIndustry_CheckedChanged" />  
                                                     <div class="company-items-wrapper">
                                                         <asp:CheckBoxList ID="chkactionIndustry"
                                                             CssClass="industry-checkboxlist" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' AutoPostBack="true" OnSelectedIndexChanged="chkactionIndustry_SelectedIndexChanged"
                                                             runat="server" RepeatDirection="Vertical"
                                                             DataTextField="Name" DataValueField="Id"   />
                                                     </div>
                                                </div> 
                                             </asp:Panel> 
                                         </td> 
                                            <td>
                                                <asp:Panel runat="server" ID="pnlRegion" CssClass="dropdown-container">
                                                    <button type="button" class="dropdown-button">Choose an item</button> 
                                                        <div class="dropdown-menu">
                                                            <input type="text" class="dropdown-search" placeholder="Search..." />
                                                        <asp:CheckBox ID="chkSelectAllRegion" ForeColor="Blue" Font-Bold="true"
                                                            runat="server" Text="Select All"  
                                                            AutoPostBack="true" OnCheckedChanged="chkSelectAllRegion_CheckedChanged" />  
                                                        <div class="company-items-wrapper">
                                                            <asp:CheckBoxList ID="chkactionRegion"
                                                                CssClass="region-checkboxlist" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' AutoPostBack="true" OnSelectedIndexChanged="chkactionRegion_SelectedIndexChanged"
                                                                runat="server" RepeatDirection="Vertical"
                                                                DataTextField="CountryName" DataValueField="CountryCode"   />
                                                        </div>
                                                    </div> 
                                                </asp:Panel> 
                                            </td>
                                            <td>
                                                <asp:Panel runat="server" ID="pnlOrganization" CssClass="dropdown-container">
                                                    <button type="button" class="dropdown-button">Choose an item</button> 
                                                        <div class="dropdown-menu">
                                                            <input type="text" class="dropdown-search" placeholder="Search..." />
                                                        <asp:CheckBox ID="chkSelectAllOrg" ForeColor="Blue" Font-Bold="true"
                                                            runat="server" Text="Select All"  
                                                            AutoPostBack="true" OnCheckedChanged="chkSelectAllOrg_CheckedChanged" />  
                                                        <div class="company-items-wrapper">
                                                            <asp:CheckBoxList ID="chkactionOrg"
                                                                CssClass="org-checkboxlist" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' AutoPostBack="true" OnSelectedIndexChanged="chkactionOrg_SelectedIndexChanged"
                                                                runat="server" RepeatDirection="Vertical"
                                                                DataTextField="Name" DataValueField="Autoid"   />
                                                        </div>
                                                    </div> 
                                                </asp:Panel> 
                                            </td>
                                             
                                            <td>
                                                <asp:Panel runat="server" ID="pnlCompany" CssClass="dropdown-container">
                                                <button type="button" class="dropdown-button">Choose an item</button> 
                                                    <div class="dropdown-menu">
                                                         <input type="text" class="dropdown-search" placeholder="Search..." />
                                                    <asp:CheckBox ID="chkselectallcompany" ForeColor="Blue" Font-Bold="true"
                                                        runat="server" Text="Select All"
                                                        AutoPostBack="true" OnCheckedChanged="chkselectallcompany_CheckedChanged" />  
                                                    <div class="company-items-wrapper">
                                                        <asp:CheckBoxList ID="chkactionCompany"
                                                            CssClass="company-checkboxlist"
                                                            runat="server" RepeatDirection="Vertical" OnSelectedIndexChanged="chkactionCompany_SelectedIndexChanged" AutoPostBack="true"
                                                            DataTextField="Name" DataValueField="Id" />
                                                    </div>
                                                </div> 
                                               </asp:Panel> 
                                            </td> 
                                            <td>
                                               <asp:Panel runat="server" ID="pnlReporting" CssClass="dropdown-container">
                                                       <button type="button" class="dropdown-button">Choose an item</button> 
                                                    <div class="dropdown-menu">
                                                            <input type="text" class="dropdown-search" placeholder="Search..." />
                                                 <asp:CheckBox ID="chkReportingPerson" ForeColor="Blue" Font-Bold="true" runat="server" Text="Select All"
                                                    AutoPostBack="true" OnCheckedChanged="chkReportingPerson_CheckedChanged" /> 
                                                          <div class="company-items-wrapper">
                                                 <asp:CheckBoxList ID="chkactioreporting" OnSelectedIndexChanged="chkactioreporting_SelectedIndexChanged" AutoPostBack="true"  CssClass="reporting-checkboxlist" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' runat="server" RepeatDirection="Vertical" DataTextField="EmpName" DataSource='<%# GetReportingperson(Eval("EmpId").ToString()) %>' DataValueField="Autoid" />
                                                                  </div>
                                                    </div> 
                                               </asp:Panel>
                                            </td>
                                            <td>
                                                <asp:CheckBoxList ID="chkLocation" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' runat="server" RepeatDirection="Vertical" DataTextField="Name" DataValueField="Id" DataSource='<%# GetLocationn( ) %>' />
                                            </td>
                                            <td> 
                                              <asp:CheckBoxList ID="chkactionMenu" class=' <%# GetInt(Container.DataItemIndex.ToString()) %>' runat="server" RepeatDirection="Vertical" DataTextField="ActionName" DataValueField="Autoid" DataSource='<%# GetAction(Eval("Autoid").ToString()) %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>
                             </ContentTemplate>
                            <Triggers> 
                                <asp:AsyncPostBackTrigger ControlID="LV_Access_Menu_Company" /> 
                            </Triggers>
                         </asp:UpdatePanel>
                    </div>
                </div>


            </div>
            <div class="form-actions right">
                <div class="row">
                    <div class="col-md-12"> 
                        <asp:Button ID="btnSave" runat="server" class="btn blue" OnClick="btnSave_Click" OnClientClick="return CheckRequiredField('req');" Text="Save" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn default" OnClick="btnCancel_Click" Text="Cancel" />
                    </div>
                </div>
            </div>
        </div>

     <div id="PopUpBulkAccess" tabindex="-1" data-width="400" class="modal fade" style="display: none">
     <div class="modal-dialog modal-lg">
         <div class="modal-content">
             <div class="modal-header bg-green">
                 <button type="button" class="close" data-dismiss="modal" aria-hidden="true"></button>
                 <h4 class="modal-title" style="color: #fff;">Apply Bulk Access</h4>
             </div>
             <div class="modal-body"> 
                                 <div class="row">
                                     <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="control-label">Group<span class="required" aria-required="true"> *</span></label>
                                           <asp:ListBox runat="server" ID="LstGroup" SelectionMode="Multiple"  CssClass="form-control select2ddl reqb"></asp:ListBox>
                                        </div>
                                    </div> 
                                     <div class="col-md-12">
                                         <div class="form-group">
                                             <label class="control-label">Industry<span class="required" aria-required="true"> *</span></label>
                                            <asp:ListBox runat="server" ID="LstIndustry"  SelectionMode="Multiple" CssClass="form-control select2ddl reqb"></asp:ListBox>
                                         </div>
                                     </div>   
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="control-label">Region<span class="required" aria-required="true"> *</span></label>
                                           <asp:ListBox runat="server" ID="LstRegion"   SelectionMode="Multiple" CssClass="form-control select2ddl reqb"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="control-label">Organization<span class="required" aria-required="true"> *</span></label>
                                           <asp:ListBox runat="server" ID="LstOrgnization"   SelectionMode="Multiple" CssClass="form-control select2ddl reqb"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="control-label">Company<span class="required" aria-required="true"> *</span></label>
                                           <asp:ListBox runat="server" ID="LstCompany"  SelectionMode="Multiple" CssClass="form-control select2ddl reqb"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="control-label">Work Location<span class="required" aria-required="true"> *</span></label>
                                           <asp:ListBox runat="server" ID="LstWorkLocation" SelectionMode="Multiple" CssClass="form-control select2ddl reqb"></asp:ListBox>
                                        </div>
                                    </div> 
                                     <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="control-label">Reporting To <span class="required" aria-required="true"> *</span></label>
                                           <asp:ListBox runat="server" ID="LstReportingTo" SelectionMode="Multiple" CssClass="form-control select2ddl reqb"></asp:ListBox>
                                        </div>
                                    </div>
                               </div>
                  <div class="modal-footer">
                        <asp:Button ID="btnaddbulkaccess" runat="server" class="btn blue" OnClick="btnaddbulkaccess_Click" OnClientClick="return CheckRequiredField('reqb');" Text="Apply" />
                      <button type="button" data-dismiss="modal" class="btn">Close</button>
                  </div>
             </div>
             <!-- END FORM-->
         </div>
     </div>
 </div>

    </div>
<asp:HiddenField ID="hidAutoidMain" runat="server" Value="" />  
<asp:HiddenField ID="hdnIndustry" runat="server" />
<asp:HiddenField ID="hdnGroup" runat="server" />
<asp:HiddenField ID="hdnRegion" runat="server" />
<asp:HiddenField ID="hdnOrganization" runat="server" />
<asp:HiddenField ID="hdnCompany" runat="server" />
<asp:HiddenField ID="hdnWorkLocation" runat="server" />
<asp:HiddenField ID="hdnReportingTo" runat="server" />

  <script type="text/javascript">
     function selectAllRegion(source) {
         debugger
         var container = source.closest(".region-container");
         if (!container) return; 
         var checkboxes = container.querySelectorAll(".checkbox-wrapper input[type='checkbox']"); 
         checkboxes.forEach(function (cb) {
             cb.checked = source.checked;
              
         }); 
     }
  </script>
    <script>
        
        $(document).on("click", ".dropdown-button", function (e) {
            e.stopPropagation();
            $(".dropdown-container").not($(this).closest(".dropdown-container")).removeClass("open");
            $(this).closest(".dropdown-container").toggleClass("open");
        }); 
       
        $(document).on("click", ".dropdown-menu", function (e) {
            e.stopPropagation();
        }); 
  
        $(document).on("click", function () {
            $(".dropdown-container").removeClass("open");
        }); 
         
    </script>
    <script> 
        if (typeof Sys !== "undefined" && Sys.WebForms && Sys.WebForms.PageRequestManager) {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                updateDropdownButtonText();
            });
        } 
        
        $(document).ready(function () {
            updateDropdownButtonText();
        }); 
        
        function updateDropdownButtonText() {
            $(".dropdown-container").each(function () {
                const $dropdown = $(this);

               
                const $checkboxLists = $dropdown.find("input[type='checkbox']").not($dropdown.find("#chkSelectAllRegion, #chkselectallcompany"));

               
                const selected = $checkboxLists.filter(":checked").map(function () {
                    return $(this).parent().text().trim();
                }).get();

                 
                const btn = $dropdown.find(".dropdown-button");
                if ($dropdown.find(".region-checkboxlist").length) {
                    btn.text(selected.length ? selected.join(", ") : "Choose an item");
                } else if ($dropdown.find(".company-checkboxlist").length) {
                    btn.text(selected.length ? selected.join(", ") : "Choose an item");
                }
                else if ($dropdown.find(".industry-checkboxlist").length) {
                    btn.text(selected.length ? selected.join(", ") : "Choose an item");
                }
                else if ($dropdown.find(".group-checkboxlist").length) {
                    btn.text(selected.length ? selected.join(", ") : "Choose an item");
                }
                else if ($dropdown.find(".org-checkboxlist").length) {
                    btn.text(selected.length ? selected.join(", ") : "Choose an item");
                }
                else if ($dropdown.find(".reporting-checkboxlist").length) {
                    btn.text(selected.length ? selected.join(", ") : "Choose an item");
                }
            });
        }
       

    </script>
    <script>
        
        $(document).on("keyup", ".dropdown-search", function () {
            const query = $(this).val().toLowerCase();
            const $wrapper = $(this).closest(".dropdown-menu").find(".company-items-wrapper");  
            $wrapper.find("label").each(function () {
                const text = $(this).text().trim().toLowerCase();
                $(this).toggle(text.indexOf(query) > -1);
            });
        });  
       
       function OpenPopUpAction() {
           $("#PopUpBulkAccess").modal({
                  backdrop: 'static',
                  keyboard: false
           });
      } 
    </script> 

    <script>
        $(document).ready(function () {

            if (typeof (__doPostBack) !== "undefined") {
                const oldPostBack = __doPostBack;
                window.__doPostBack = function (eventTarget, eventArgument) {
                    // Block postback for our dropdowns
                    if (eventTarget && (
                        eventTarget.includes("LstIndustry") ||
                        eventTarget.includes("LstGroup") ||
                        eventTarget.includes("LstRegion") ||
                        eventTarget.includes("LstOrgnization") ||
                        eventTarget.includes("LstCompany") ||
                        eventTarget.includes("LstReportingTo")
                    )) {
                        console.log("Prevented postback for: " + eventTarget);
                        return false;
                    }

                    return oldPostBack(eventTarget, eventArgument);
                };
            }

            $('.select2ddl').select2({
                placeholder: "Select",
                width: '100%'
            });

            // 🔹 Helper to populate ListBox
            function populateListBox(selector, items) {
                var lst = $(selector);
                lst.empty();
                $.each(items, function (i, item) {
                    lst.append($("<option>")
                        .val(item.Id || item.Autoid || item.CountryCode)
                        .text(item.Name || item.CountryName));
                });
                lst.trigger('change.select2'); // refresh UI
                updateHiddenField(selector); // update hidden field whenever list changes
            }

            // 🔹 Helper to update hidden fields
            function updateHiddenField(listBoxSelector) {
                var values = $(listBoxSelector).val() || [];
                switch (listBoxSelector) {
                    case "#<%= LstIndustry.ClientID %>":
                    $("#<%= hdnIndustry.ClientID %>").val(values.join(","));
                    break;
                case "#<%= LstGroup.ClientID %>":
                    $("#<%= hdnGroup.ClientID %>").val(values.join(","));
                    break;
                case "#<%= LstRegion.ClientID %>":
                    $("#<%= hdnRegion.ClientID %>").val(values.join(","));
                    break;
                case "#<%= LstOrgnization.ClientID %>":
                    $("#<%= hdnOrganization.ClientID %>").val(values.join(","));
                    break;
                case "#<%= LstCompany.ClientID %>":
                    $("#<%= hdnCompany.ClientID %>").val(values.join(","));
                    break;
                case "#<%= LstWorkLocation.ClientID %>":
                    $("#<%= hdnWorkLocation.ClientID %>").val(values.join(","));
                        break;
                case "#<%= LstReportingTo.ClientID %>":
                        $("#<%= hdnReportingTo.ClientID %>").val(values.join(","));
                        break;

            }
        }

        // 🔹 Update hidden field on manual change
        $(".select2ddl").on("change", function () {
            updateHiddenField("#" + $(this).attr("id"));
        });


            // 🔹 Group -> Region
        $("#<%= LstGroup.ClientID %>").on("change", function (e) {
            e.preventDefault(); e.stopImmediatePropagation();
            var selected = $(this).val() || [];;
            $.ajax({
                type: "POST",
                url: "RBACHRMS.aspx/GetIndustriess",
                data: JSON.stringify({ groupIds: selected.join(",") }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    populateListBox("#<%= LstIndustry.ClientID %>", response.d);
                }
            });
            return false;
        });

        // 🔹 Industry -> Group
        $("#<%= LstIndustry.ClientID %>").on("change", function (e) {
            e.preventDefault(); e.stopImmediatePropagation();  
            var selected = $(this).val() || [];
            $.ajax({
                type: "POST",
                url: "RBACHRMS.aspx/GetRegions",
                data: JSON.stringify({ industryIds: selected.join(",") }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    populateListBox("#<%= LstRegion.ClientID %>", response.d);
                }
            });
            return false;
        });

       

        // 🔹 Region -> Organization
        $("#<%= LstRegion.ClientID %>").on("change", function (e) {
            e.preventDefault(); e.stopImmediatePropagation();
            var selected = $(this).val() || [];
            var selectedGroups = $("#<%= LstGroup.ClientID %>").val() || [];
            var selectedInds = $("#<%= LstIndustry.ClientID %>").val() || [];
            $.ajax({
                type: "POST",
                url: "RBACHRMS.aspx/GetOrganizations",
                data: JSON.stringify({
                    regionIds: selected.join(","),
                    groupIds: selectedGroups.join(","),
                    indIds: selectedInds.join(",")
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    populateListBox("#<%= LstOrgnization.ClientID %>", response.d);
                }
            });
            return false;
        });

        // 🔹 Organization -> Company
        $("#<%= LstOrgnization.ClientID %>").on("change", function (e) {
            e.preventDefault(); e.stopImmediatePropagation();
            var selected = $(this).val() || [];
            $.ajax({
                type: "POST",
                url: "RBACHRMS.aspx/GetCompanies",
                data: JSON.stringify({ orgIds: selected.join(",") }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    populateListBox("#<%= LstCompany.ClientID %>", response.d);
                }
            });
            return false;
        });

    });
    </script>



  <style>
 .btn-access {
    font-weight: 600;
    letter-spacing: 0.5px;
    transition: transform 0.2s, box-shadow 0.2s;
}
.btn-access:hover {
    transform: translateY(-2px);
    box-shadow: 0 4px 10px rgba(0,0,0,0.2);
}
.dropdown-container {
    position: relative;
    display: inline-block;
    width: 220px; 
    font-family: Arial, sans-serif;
}
 
.dropdown-button {
    width: 100%;
    background-color: #fff;
    border: 1px solid #ccc;
    padding: 6px 10px;
    text-align: left;
    border-radius: 6px;
    cursor: pointer;
    position: relative;
    font-size: 13px;
    white-space: nowrap;
    overflow: hidden;
    text-overflow: ellipsis;  
} 
.dropdown-button::after {
    content: "▼";
    position: absolute;
    right: 10px;
    top: 6px;
    font-size: 10px;
    color: #555;
}

.dropdown-container.open .dropdown-button::after {
    content: "▲";
}  
.dropdown-menu {
    display: none;
    position: absolute;
    background-color: #fff;
    border: 1px solid #ccc;
    box-shadow: 0 2px 6px rgba(0,0,0,0.12);
    z-index: 10;
    width: 100%; 
    max-height: 200px; 
    overflow-y: auto;  
    padding: 8px;
    border-radius: 6px;
} 
 
.dropdown-container.open .dropdown-menu {
    display: block;
} 
 
.company-checkboxlist label {
    display: inline;
    padding: 3px 0;
    font-size: 13px;
} 
 
.dropdown-button {
    max-width: 250px;  
}

.dropdown-search {
    width: 100%;
    padding: 4px 6px;
    margin-bottom: 6px;
    border: 1px solid #ccc;
    border-radius: 4px;
    font-size: 13px;
    box-sizing: border-box;
} 
 .modal
{
    z-index : 1050 !important;
}
 .modal-backdrop {
    border: 0;
    outline: none;
    z-index: 1000 !important;
}
</style> 
</asp:Content>



