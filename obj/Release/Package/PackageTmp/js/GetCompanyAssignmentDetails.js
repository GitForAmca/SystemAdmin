//Get Company Details
function ViewCompanydetails(args) {
    debugger 
    var Autoid = $(args).attr('Autoid');
    var Data = JSON.stringify({ Autoid: Autoid });
    $.ajax({
        url: "../detailsGeneral.aspx/View_CompanyDetails",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var jsondata = JSON.parse(mydata.d);
            $.each(jsondata, function (key, value) {
                debugger
                $("#ClientId").html(value.ClientUId);
                if (value.DataType != '') {
                    if (value.DataType == '1') {
                        $("#compayName").html(value.PrimaryContactPerson);
                    }
                    else {
                        $("#compayName").html(value.Name);
                    }
                }

                $("#ContactPerson").html("<i class='fa fa-user-o styleico' aria-hidden='true'></i> " + value.NameTitle + " " + value.PrimaryContactPerson + " - " + value.Designation);
                var secondaryConcernPerson = '';
                secondaryConcernPerson = value.SecondaryClientDetails;
                $("#ContactPersonSecondary").css("display", "none");
                if (secondaryConcernPerson != '') {
                    $("#ContactPersonSecondary").html("<i class='fa fa-users styleico' aria-hidden='true'></i> " + secondaryConcernPerson);
                    $("#ContactPersonSecondary").css("display", "block");
                }

                $("#EmialId").html("<i class='fa fa-envelope-o styleico' aria-hidden='true'></i> " + value.ClientEmail);
                $("#ContactDetails").html("<i class='fa fa-phone styleico' aria-hidden='true'></i> " + value.CountryCode + " " + value.PrimaryContactDetail);
                
                if (value.Location != '') {
                    $("#authority").html("<i class='fa fa-home styleico' aria-hidden='true'></i> " + value.Location);
                    $("#authority").css("display", "block");
                }

                $("#address").html("<i class='fa fa-location-arrow styleico' aria-hidden='true'></i> " + "Unit No: " + value.SuitNo + ", Floor No: " + value.FloorNo + ", Tower: " + value.tower + ", Address: " + value.AddressArea + ", Emirates: " + value.Area);

                getPreviousAuditorList(Autoid);
                getClientAssignmentList(Autoid);
                getAssignmentDropdown(Autoid);
                getEntityCategory(value.Attribute1, "Category");
                $("#divReportLogs").html('');
            });
            $("#PopUpCompanyDetails").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    });
}
function ViewConcernedPerson(args) {
    debugger
    var Autoid = $(args).attr('Autoid');
    var Data = JSON.stringify({ Autoid: Autoid });
    $.ajax({
        url: "../detailsGeneral.aspx/GetConcernedPerson",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = " <table class='table table-bordered'>";
            row += "<thead><tr style='background: #666; color: #fff'><th>Concern Person</th><th>Nationality</th><th>Designation</th><th>Email Id</th><th>Contact No</th></tr></thead>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                row += "<tbody>";
                $.each(jsondata, function (key, value) {
                    row += "<tr><td>" + value.NameTitle + " " + value.Name + "</td><td>" + value.Nationality + "</td><td>" + value.Designation + "</td><td>" + value.EmailId + "</td><td>" + + value.CountryCode + " " + value.MobileNo + "</td></tr>";
                });
                row += "</tbody>";
            }
            row += "</table>";
            $("#divGetConcernedPerson").html(row);
            $("#PopUpConcernedPerson").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    });
    $("#divGetConcernedPerson").html('');
}
function getClientAssignmentList(id) {
    var Data = JSON.stringify({ Autoid: id });
    $.ajax({
        url: "../detailsGeneral.aspx/GetClientAssignmentLogs",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = " <table class='table table-bordered'>";
            row += "<thead><tr style='background: #666; color: #fff'><th>ID</th><th>Name</th><th>Period</th><th>CS</th><th>Status</th><th>Date</th></tr></thead>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                row += "<tbody>";
                $.each(jsondata, function (key, value) {
                    row += "<tr><td>" + value.AuditSysgenId + "</td><td>" + value.ServiceName + "</td><td>" + value.PeriodFrom + ' To ' + value.PeriodTo + "</td><td style='width: 200px'><span style='display: inline-block; width: 40px; background: #ddd; padding: 0px 4px; margin-top: 2px'>P:</span> " + value.PrimaryCSR + "<br><span style='display: inline-block; width: 40px; background: #ddd; padding: 0px 4px; margin-top: 2px'>S:</span> " + value.SecondaryCSR + "</td><td>" + value.AutomaticStatus + "</td><td>" + value.AutomaticStatusDate + "</td></tr>";
                });
                row += "</tbody>";
            }
            row += "</table>";
            $("#divGetClientAssignmentLogs").html(row);
        }
    });
    $("#divGetClientAssignmentLogs").html('');
}
function getAssignmentDropdown(id) {
    var Data = JSON.stringify({ Autoid: id });
    $.ajax({
        url: "../detailsGeneral.aspx/GetClientAssignmentLogs",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = "<option>Select Option</option>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                $.each(jsondata, function (key, value) {
                    row += "<option value=" + value.Autoid + ">" + value.AuditSysgenIdServiceName + "</option>";
                });
            }
            $("#getReportLogs").html(row);
        }
    });
    $("#getReportLogs").html('');
}
$('#getReportLogs').on('change', function () {
    getFullReportLogs(this.value);
});
function getFullReportLogs(id) {
    var Data = JSON.stringify({ Autoid: id });
    $.ajax({
        url: "../detailsGeneral.aspx/GetAssignmentReportLogs",
        data: Data,
        type: "POST",
        dataType: "json", 
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = " <table class='table table-bordered'>";
            row += "<thead><tr style='background: #666; color: #fff'><th>ID</th><th>Period</th><th>Status</th><th>Date</th></tr></thead>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                row += "<tbody>"; 
                $.each(jsondata, function (key, value) {
                    row += "<tr><td>" + value.ReportsNo + "</td><td>" + value.PeriodFrom + " To " + value.PeriodTo + "</td><td>" + value.AutomaticStatus + "</td><td>" + value.AutomaticStatusDate + "</td></tr>";
                });
                row += "</tbody>";
            }
            row += "</table>";
            $("#divReportLogs").html(row);
        }
    });
    $("#divReportLogs").html('');
}
function ViewAssignmentDetails(Autoid) {
    $('[data-toggle="tooltip"]').tooltip();
    var Data = JSON.stringify({ Autoid: Autoid });
    $.ajax({
        url: "../detailsGeneral.aspx/getAssignment",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            debugger
            var jsondata = JSON.parse(mydata.d);
            $("#lblAssignmentId").html(jsondata[0].AuditSysgenId);
            $("#lblAssignmentName").html(jsondata[0].ServiceName);
            $("#lblFullPeriod").html("<i class='fa fa-calendar iconL' data-toggle='tooltip' data-placement='top' title='Period' aria-hidden='true'></i> " + jsondata[0].PeriodFrom + " To " + jsondata[0].PeriodTo);
            if (jsondata[0].AssignmentEndBy != null) {
                $("#lblCompletedBy").html("<i class='fa fa-user iconL' data-toggle='tooltip' data-placement='top' title='Assignment End By' aria-hidden='true'></i> " + jsondata[0].AssignmentEndBy);
            }
            if (jsondata[0].AssignmentEnddate != null) {
                $("#lblCompletedOn").html("<i class='fa fa-hourglass-end iconL' data-toggle='tooltip' data-placement='top' title='Assignment End On' aria-hidden='true'></i> " + jsondata[0].AssignmentEnddate);
            }
            
            
        }
    });
}
//Get Company Details
function ViewGetFullReport(args) {
    debugger
    $("#divReportLogs").html('');
    var Autoid = $(args).attr('Autoid');
    getFullReportLogs(Autoid);
    ViewAssignmentDetails(Autoid);
    $("#PopUpReportDetails").modal({
        backdrop: 'static',
        keyboard: false
    });
}

function getPreviousAuditorList(id) {
    var Data = JSON.stringify({ Autoid: id });
    $.ajax({
        url: "../detailsGeneral.aspx/GetClientAuditorLogs",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = " <table class='table table-bordered'>";
            row += "<thead><tr style='background: #666; color: #fff'><th>ID</th><th>Operations</th><th>CS</th><th>Status</th><th>Assignment</th><th>Date</th></tr></thead>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                row += "<tbody>";
                $.each(jsondata, function (key, value) {
                    row += "<tr><td>" + value.AuditSysgenId + "</td><td><span style='display: inline-block; width: 20px; background: #ddd; padding: 0px 4px'>P: </span>" + value.PrimaryAuditor + "<br><span style='display: inline-block; width: 20px; background: #ddd; padding: 0px 4px'>S: </span>" + value.SecondaryAuditor + "</td><td><span style='display: inline-block; width: 20px; background: #ddd; padding: 0px 4px'>P: </span>" + value.PrimaryCSR + "<br><span style='display: inline-block; width: 20px; background: #ddd; padding: 0px 4px'>S: </span>" + value.SecondaryCSR + "</td><td>" + value.AutomaticStatus + "</td><td>" + value.ServiceName + "</td><td><span style='display: inline-block; width: 80px; background: #ddd; padding: 0px 4px'>Opened: </span>" + value.FileOpeningDate + "<br><span style='display: inline-block; width: 80px; background: #ddd; padding: 0px 4px'>Assigned: </span>" + value.AssignAuditorDate + "<br><span style='display: inline-block; width: 80px; background: #ddd; padding: 0px 4px'>Completed: </span>" + value.CompletedOn + "</td></tr>";
                });
                row += "</tbody>";
            }
            row += "</table>";
            $("#divGetClientAuditorLogs").html(row);
        }
    });
    $("#divGetClientAuditorLogs").html('');
}
function ViewAssignmentAuditorLogs(args) {
    debugger
    var AuditSysGenId = $(args).attr('AuditSysgenId');
    var FileId = $(args).attr('FileId');
    var Data = JSON.stringify({ AuditSysGenId: AuditSysGenId });
    $.ajax({
        url: "../detailsGeneral.aspx/GetAssignmentAuditorLogs",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = " <table class='table table-bordered'>";
            row += "<thead><tr style='background: #666; color: #fff'><th>Auditor's Name</th><th>Assign On</th></tr></thead>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                row += "<tbody>";
                $.each(jsondata, function (key, value) {
                    row += "<tr><td>" + value.OldAuditorName + "</td><td>" + value.OldAuditorAssignDate + "</td></tr>";
                });
                row += "</tbody>";
            }
            row += "</table>";
            getFullReportLogs(FileId);
            $("#divAssignmentAuditorLogs").html(row);
            getAssignmentCategory(FileId, "Category");
            $("#PopUpAssignmentDetails").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    });
    $("#divAssignmentAuditorLogs").html('');
}
function ViewFullCompanydetails(args) {
    debugger 
    var Autoid = $(args).attr('Autoid');
    var Data = JSON.stringify({ Autoid: Autoid });
    $.ajax({
        url: "../detailsGeneral.aspx/View_CompanyDetails",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var jsondata = JSON.parse(mydata.d);
            var row = "";
            $("#divSummary").html(row);
            $.each(jsondata, function (key, value) {
                debugger
                $("#ClientIdFull").html(value.ClientUId);
                if (value.DataType != '') {
                    if (value.DataType == '1') {
                        $("#compayNameFull").html(value.PrimaryContactPerson);
                    }
                    else {
                        $("#compayNameFull").html(value.Name);
                    }
                }

                $("#ContactPerson").html("<i class='fa fa-user-o styleico' aria-hidden='true'></i> " + value.NameTitle + " " + value.PrimaryContactPerson + " - " + value.Designation);
                var secondaryConcernPerson = '';
                secondaryConcernPerson = value.SecondaryClientDetails;
                $("#ContactPersonSecondary").css("display", "none");
                if (secondaryConcernPerson != '') {
                    $("#ContactPersonSecondary").html("<i class='fa fa-users styleico' aria-hidden='true'></i> " + secondaryConcernPerson);
                    $("#ContactPersonSecondary").css("display", "block");
                }

                $("#EmialId").html("<i class='fa fa-envelope-o styleico' aria-hidden='true'></i> " + value.ClientEmail);
                $("#ContactDetails").html("<i class='fa fa-phone styleico' aria-hidden='true'></i> " + value.CountryCode + " " + value.PrimaryContactDetail);
                if (value.Location != '') {
                    $("#authorityFull").html("<i class='fa fa-home styleico' aria-hidden='true'></i> " + value.Location);
                    $("#authorityFull").css("display", "block");
                }

                $("#address").html("<i class='fa fa-location-arrow styleico' aria-hidden='true'></i> " + "Unit No: " + value.SuitNo + ", Floor No: " + value.FloorNo + ", Tower: " + value.tower + ", Address: " + value.AddressArea + ", Emirates: " + value.Area);
            });
            $("#PopUpFullCompanyDetails").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    });
    $("#divSummaryFull").html('');
}
//Get Company Details from PL
function ViewCompanydetailsPL(args) {
    debugger
    var Autoid = $(args).attr('Autoid');
    var Data = JSON.stringify({ Autoid: Autoid });
    $.ajax({
        url: "../detailsGeneral.aspx/View_PLCompanyDetails",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var jsondata = JSON.parse(mydata.d);
            var row = "";
            $("#divSummary").html(row);
            $.each(jsondata, function (key, value) {
                debugger
                $("#ClientId").html(value.PLClientId);
                if (value.DataType != '') {
                    if (value.DataType == '1') {
                        $("#compayName").html(value.PRIMARY_CONTACT_PERSON);
                    }
                    else {
                        $("#compayName").html(value.COMPANY_NAME);
                    }
                }

                $("#ContactPerson").html("<i class='fa fa-user-o styleico' aria-hidden='true'></i> " + value.NameTitle + " " + value.PRIMARY_CONTACT_PERSON + " - " + value.PRIMARY_DESIGNATION);
                if (value.SecondaryClientDetails != null) {
                    $("#ContactPersonSecondary").html("<i class='fa fa-users styleico' aria-hidden='true'></i> " + value.SecondaryClientDetails);
                    $("#ContactPersonSecondary").css("display", "block");
                }

                $("#EmialId").html("<i class='fa fa-envelope-o styleico' aria-hidden='true'></i> " + value.PRIMARY_EMAIL_ID);
                $("#ContactDetails").html("<i class='fa fa-phone styleico' aria-hidden='true'></i> " + value.CountryCode + " " + value.PRIMARY_MOBILE_NO);
                $("#Location").html("<i class='fa fa-map-marker styleico' aria-hidden='true'></i>  <a  Target='_blank' href='" + value.LocationURL + "' >" + value.LocationURL +"</a>");

                if (value.Trade_License_Authority != '') {
                    $("#authority").html("<i class='fa fa-home styleico' aria-hidden='true'></i> " + value.Trade_License_Authority);
                    $("#authority").css("display", "block");
                }

                $("#address").html("<i class='fa fa-location-arrow styleico' aria-hidden='true'></i> " + "Unit No: " + value.UNIT_NO + ", Floor No: " + value.FLOOR + ", Tower: " + value.tower + ", Address: " + value.Area + ", Emirates: " + value.Emirates);
            });
            $("#PopUpCompanyDetails").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    });
    $("#divSummary").html('');
}
//Get Consultant Details
function ViewConsultantdetails(args) {
    debugger
    var Autoid = $(args).attr('Autoid');
    var Data = JSON.stringify({ Autoid: Autoid });
    $.ajax({
        url: "../detailsGeneral.aspx/View_ConsultantDetails",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var jsondata = JSON.parse(mydata.d);
            var row = "";
            $("#divConsultantSummary").html(row);
            $.each(jsondata, function (key, value) {
                debugger
                $("#ConsultantId").html(value.ConsultantId);
                $("#ConsultantName").html(value.ConsultantName);
                $("#ConsultantContactPerson").html("<i class='fa fa-user-o styleico' aria-hidden='true'></i> " + value.NameTitle + " " + value.ConcernPerson + " - " + value.Designation);
                if (value.SecondaryConsultantDetails != null) {
                    $("#ConsultantContactPersonSecondary").html("<i class='fa fa-users styleico' aria-hidden='true'></i> " + value.SecondaryConsultantDetails);
                    $("#ConsultantContactPersonSecondary").css("display", "block");
                }

                $("#ConsultantEmialId").html("<i class='fa fa-envelope-o styleico' aria-hidden='true'></i> " + value.EmailId);
                $("#ConsultantContactDetails").html("<i class='fa fa-phone styleico' aria-hidden='true'></i> " + value.CountryCode + " " + value.ContactDetails);
                $("#Consultantaddress").html("<i class='fa fa-location-arrow styleico' aria-hidden='true'></i> " + "Unit No: " + value.UnitNo + ", Floor No: " + value.FloorNo + ", Tower: " + value.Tower + ", Address: " + value.ConsultantAddress + ", Emirates: " + value.Emirates);
            });
            $("#PopUpConsultantDetails").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    });
    $("#divConsultantSummary").html('');
}
//Get Assignment Status
function ViewFileStatus(args) {
    debugger
    var FileNo = $(args).attr('FileNo');
    var AssignmentId = $(args).attr('Autoid');
    var Data = JSON.stringify({ FileNo: FileNo });
    $.ajax({
        url: "../detailsGeneral.aspx/GetFileStatus",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = " <table class='table table-bordered'>";
            row += "<thead><tr style='background: #666; color: #fff'><th>Status</th><th>Updated On</th><th>Updated By</th></tr></thead>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                row += "<tbody>";
                $.each(jsondata, function (key, value) {
                    row += "<tr><td>" + value.Statusname + "</td><td>" + value.CreatedOn + "</td><td>" + value.CreatedBy + "</td></tr>";
                    $("#FileNo").html(value.AuditSysgenId);
                    $("#lblServiceNamePop").html(value.Servicename);
                    $("#PeriodFrom").html(value.PeriodFrom);
                    $("#PeriodTo").html(value.PeriodTo);
                });
                row += "</tbody>";
            }
            row += "</table>";
            $("#divFileStatus").html(row);
            $("#popupFileStatus").modal({
                backdrop: 'static',
                keyboard: false
            });
            GetReportList(AssignmentId);
        }
    });
    $("#divFileStatus").html('');
    $("#divOperationsStatus").html('');
    $("#divAccountsStatus").html('');
    $("#divAdminStatus").html('');
    $(".divdepartmentstatus").css("display", "none");
}
//Get Report List
function GetReportList(id) {
    debugger
    var Data = JSON.stringify({ Autoid: id });
    $.ajax({
        url: "../detailsGeneral.aspx/GetReportListAjax",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = "<option value=''>Select Option</option>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                $.each(jsondata, function (key, value) {
                    row += "<option value=" + value.Autoid + ">" + value.ReportsNo + "</option>";
                });
            }
            $("#ddlReportList_selectedChange").html(row);
        }
    });
    $("#ddlReportList_selectedChange").html('');
}
$('#ddlReportList_selectedChange').on('change', function () {
    debugger
    var reportSelected = $("#ddlReportList_selectedChange option:selected").val();
    getAccountsReportStatus(reportSelected);
    getOperationReportStatus(reportSelected);
    getAdminReportStatus(reportSelected);
    $(".divdepartmentstatus").css("display", "block"); 
});
 
function getReportStatusIndividual(args) {
    debugger
    var Autoid = $(args).attr('Autoid');
    getAccountsReportStatus(Autoid);
    getOperationReportStatus(Autoid);
    getAdminReportStatus(Autoid);
    $("#popupReportStatus").modal({
        backdrop: 'static',
        keyboard: false
    });
}
function getAccountsReportStatus(id) {
    var Data = JSON.stringify({ Autoid: id });
    $.ajax({
        url: "../detailsGeneral.aspx/GetReportStatus",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = " <table class='table table-bordered'>";
            row += "<thead><tr style='background: #666; color: #fff'><th>Period</th><th>Status</th><th>Remarks</th><th>Updated On</th><th>Updated By</th></tr></thead>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                row += "<tbody>";
                $.each(jsondata, function (key, value) {
                    row += "<tr><td>" + value.PeriodFrom + " " + value.PeriodTo + "</td><td>" + value.Status + "</td><td>" + value.Comment + "</td><td>" + value.CreatedOn + "</td><td>" + value.CreatedBy + "</td></tr>";
                });
                row += "</tbody>";
            }
            row += "</table>";
            $("#divAccountsStatus").html(row);

        }
    });
    $("#divAccountsStatus").html('');
}


function getOperationReportStatus(id) {
    var Data = JSON.stringify({ Autoid: id });
    $.ajax({
        url: "../detailsGeneral.aspx/GetReportOperationStatus",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = " <table class='table table-bordered'>";
            row += "<thead><tr style='background: #666; color: #fff'><th>Period</th><th>Status</th><th>Remarks</th><th>Updated On</th><th>Updated By</th></tr></thead>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                row += "<tbody>";
                $.each(jsondata, function (key, value) {
                    row += "<tr><td>" + value.PeriodFrom + " " + value.PeriodTo + "</td><td>" + value.Status + "</td><td>" + value.Comment + "</td><td>" + value.CreatedOn + "</td><td>" + value.CreatedBy + "</td></tr>";
                });
                row += "</tbody>";
            }
            row += "</table>";
            $("#divOperationsStatus").html(row);

        }
    });
    $("#divOperationsStatus").html('');
}
function getAdminReportStatus(id) {
    var Data = JSON.stringify({ Autoid: id });
    $.ajax({
        url: "../detailsGeneral.aspx/GetReportAdminStatus",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = " <table class='table table-bordered'>";
            row += "<thead><tr style='background: #666; color: #fff'><th>Period</th><th>Status</th><th>Remarks</th><th>Updated On</th><th>Updated By</th></tr></thead>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                row += "<tbody>";
                $.each(jsondata, function (key, value) {
                    row += "<tr><td>" + value.PeriodFrom + " " + value.PeriodTo + "</td><td>" + value.Status + "</td><td>" + value.Comment + "</td><td>" + value.CreatedOn + "</td><td>" + value.CreatedBy + "</td></tr>";
                });
                row += "</tbody>";
            }
            row += "</table>";
            $("#divAdminStatus").html(row);
        }
    });
    $("#divAdminStatus").html('');
}

//Get Company Details from Data master
function ViewCompanydetailsDataMaster(args) {
    var Autoid = $(args).attr('Autoid');
    var Data = JSON.stringify({ Autoid: Autoid });
    $.ajax({
        url: "../detailsGeneral.aspx/ViewCompanydetailsDataMaster",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            debugger
            var jsondata = JSON.parse(mydata.d);
            $("#ClientId").html(jsondata[0].DMSysId);
            if (jsondata[0].DataType != '') {
                if (jsondata[0].DataType == '1') {
                    $("#compayName").html(jsondata[0].CONTACT_PERSON);
                }
                else {
                    $("#compayName").html(jsondata[0].COMPANY_NAME);
                    $("#LandlineDetails").html("<i class='fa fa-phone styleico' aria-hidden='true'></i> " + jsondata[0].CountryCodeLandline + " " + jsondata[0].LANDLINE);
                    $("#LandlineDetails").css("display", "block");
                }
            }
            $("#ContactPerson").html("<i class='fa fa-user-o styleico' aria-hidden='true'></i> " + jsondata[0].NameTitle + " " + jsondata[0].CONTACT_PERSON + " - " + jsondata[0].Designation);
            $("#EmialId").html("<i class='fa fa-envelope-o styleico' aria-hidden='true'></i> " + jsondata[0].EMAILID);
            $("#ContactDetails").html("<i class='fa fa-mobile styleico' aria-hidden='true'></i> " + jsondata[0].CountryCode + " " + jsondata[0].MOBILE_NO);
            if (jsondata[0].AUTHORITY != '') {
                $("#authority").html("<i class='fa fa-home styleico' aria-hidden='true'></i> " + jsondata[0].AUTHORITY);
                $("#authority").css("display", "block");
            }
            $("#address").html("<i class='fa fa-location-arrow styleico' aria-hidden='true'></i> " + "Unit No: " + jsondata[0].UnitNo + ", Floor No: " + jsondata[0].Floor
                + ", Tower: " + jsondata[0].Tower + ", Address: " + jsondata[0].Area + ", Emirates: " + jsondata[0].Emirates);
            $("#PopUpCompanyDetails").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    });
}
//Get Company Details Lead Master
function ViewCompanydetailsLeadMaster(args) {
    var Autoid = $(args).attr('Autoid');
    var Data = JSON.stringify({ Autoid: Autoid });
    $.ajax({
        url: "../detailsGeneral.aspx/ViewCompanydetailsLeadMaster",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            debugger
            var jsondata = JSON.parse(mydata.d);
            $("#ClientId").html(jsondata[0].LMSysId);
            if (jsondata[0].DataType != '') {
                if (jsondata[0].DataType == '1') {
                    $("#compayName").html(jsondata[0].CONTACT_PERSON);
                }
                else {
                    $("#compayName").html(jsondata[0].COMPANY_NAME);
                    $("#LandlineDetails").html("<i class='fa fa-phone styleico' aria-hidden='true'></i> " + jsondata[0].CountryCodeLandline + " " + jsondata[0].LANDLINE);
                    $("#LandlineDetails").css("display", "block");
                }
            }
            $("#ContactPerson").html("<i class='fa fa-user-o styleico' aria-hidden='true'></i> " + jsondata[0].NameTitle + " " + jsondata[0].CONTACT_PERSON + " - " + jsondata[0].Designation);
            $("#EmialId").html("<i class='fa fa-envelope-o styleico' aria-hidden='true'></i> " + jsondata[0].EMAILID);
            $("#ContactDetails").html("<i class='fa fa-mobile styleico' aria-hidden='true'></i> " + jsondata[0].CountryCode + " " + jsondata[0].MOBILE_NO);
            if (jsondata[0].AUTHORITY != '') {
                $("#authority").html("<i class='fa fa-home styleico' aria-hidden='true'></i> " + jsondata[0].AUTHORITY);
                $("#authority").css("display", "block");
            }
            $("#address").html("<i class='fa fa-location-arrow styleico' aria-hidden='true'></i> " + "Unit No: " + jsondata[0].UnitNo + ", Floor No: " + jsondata[0].Floor
                + ", Tower: " + jsondata[0].Tower + ", Address: " + jsondata[0].Area + ", Emirates: " + jsondata[0].Emirates);
            $("#PopUpCompanyDetails").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    });
}
function ViewCompanyDetailsDMLead(args) {
    var Autoid = $(args).attr('Autoid');
    var Data = JSON.stringify({ Autoid: Autoid });
    $.ajax({
        url: "../detailsGeneral.aspx/ViewCompanydetailsDMLead",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            debugger
            var jsondata = JSON.parse(mydata.d);
            $("#ClientId").html(jsondata[0].UniqueLeadId);
            if (jsondata[0].Data_TypeID != '') {
                if (jsondata[0].Data_TypeID == '1') {
                    $("#compayName").html(jsondata[0].ConcernPerson);
                }
                else {
                    $("#compayName").html(jsondata[0].CompanyName);
                }
            }
            $("#ContactPerson").html("<i class='fa fa-user-o styleico' aria-hidden='true'></i> " + jsondata[0].ConcernPerson);
            $("#EmialId").html("<i class='fa fa-envelope-o styleico' aria-hidden='true'></i> " + jsondata[0].EmailId);
            $("#ContactDetails").html("<i class='fa fa-mobile styleico' aria-hidden='true'></i> " + jsondata[0].CountryCodeContact + " " + jsondata[0].ContactNumber);
            if (jsondata[0].TradeLicenseAuthority != '') {
                $("#authority").html("<i class='fa fa-home styleico' aria-hidden='true'></i> " + jsondata[0].TradeLicenseAuthority);
                $("#authority").css("display", "block");
            }
            $("#source").html("<i class='fa fa-list styleico' aria-hidden='true'></i> " + jsondata[0].AboutAMCA);
            $("#enquiryApproach").html("<i class='fa fa-cubes styleico' aria-hidden='true'></i> " + jsondata[0].EnquiryApproach);
            $("#PopUpCompanyDetails").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    });
}
function ViewCompanyDetailsTDMLead(args) {
    var Autoid = $(args).attr('Autoid');
    var Data = JSON.stringify({ Autoid: Autoid });
    $.ajax({
        url: "../detailsGeneral.aspx/ViewCompanydetailsTDMLead",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            debugger
            var jsondata = JSON.parse(mydata.d);
            $("#ClientId").html(jsondata[0].UniqueLeadId);
            if (jsondata[0].Data_TypeID != '') {
                if (jsondata[0].Data_TypeID == '1') {
                    $("#compayName").html(jsondata[0].CompanyName);
                    $("#ContactPerson").html("<i class='fa fa-user-o styleico' aria-hidden='true'></i> " + jsondata[0].ConcernPerson);
                }
                else {
                    $("#compayName").html(jsondata[0].CompanyName);
                    $("#ContactPerson").html("<i class='fa fa-user-o styleico' aria-hidden='true'></i> " + jsondata[0].FirstName + " " + jsondata[0].MiddleName + " " + jsondata[0].LastName);
                }
            }
            $("#EmialId").html("<i class='fa fa-envelope-o styleico' aria-hidden='true'></i> " + jsondata[0].EmailId);
            $("#ContactDetails").html("<i class='fa fa-mobile styleico' aria-hidden='true'></i> " + jsondata[0].CountryCodeContact + " " + jsondata[0].ContactNumber);
            if (jsondata[0].TradeLicenseAuthority != '') {
                $("#authority").html("<i class='fa fa-home styleico' aria-hidden='true'></i> " + jsondata[0].TradeLicenseAuthority);
                $("#authority").css("display", "block");
            }
            $("#source").html("<i class='fa fa-list styleico' aria-hidden='true'></i> " + jsondata[0].AboutAMCA);
            $("#enquiryApproach").html("<i class='fa fa-cubes styleico' aria-hidden='true'></i> " + jsondata[0].EnquiryApproach);
            $("#PopUpCompanyDetails").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    });
}

function ViewSummary(hidautoid, menuid, type) {
    debugger
    ViewRemarks(hidautoid, menuid, type);
    $("#PopUpRemarks").modal({
        backdrop: 'static',
        keyboard: false
    });
}
function ViewRemarks(hidautoid, menuid, type) {
    debugger
    var Autoid = hidautoid;
    var Data = JSON.stringify({ Autoid: Autoid, MenuId: menuid, Type: type });
    $.ajax({
        url: "../detailsGeneral.aspx/View_Remarks",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            debugger
            var row = "";
            row += "<table class='table table-bordered TableAdvanceSearch' id='StatusTable'>";
            row += "<thead><tr style='background: #444d58; color: #fff'><th rowspan='yes' style='padding: 4px 10px;'>Status</th><th rowspan='yes' style='padding: 4px 10px;'>Type</th><th rowspan='yes' style='padding: 4px 10px;'>Remarks</th><th rowspan='yes' style='padding: 4px 10px;'>Follow-up On</th><th style='padding: 4px 10px;' rowspan='yes'>Created By</th><th style='padding: 4px 10px;' rowspan='yes'>Created On</th></tr></thead>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                $.each(jsondata, function (key, value) {
                    row += "<tr><td width='10%' class='colTime'>" + value.StatusName + "</td><td width='10%' class='colTime'>" + value.Action + "</td><td width='60%'>" + value.Remakrs + "</td><td width='10%'>" + (row.NextFollowUpOn && row.NextFollowUpOn !== '01-Jan-1900' ? row.NextFollowUpOn : '') + "</td><td width='10%'>" + value.CreatedBy + "</td><td width='10%'>" + value.CreatedOn + "</td></tr>";
                });
            }

            row += "</table>";
            $("#divViewRemarks").html(row);

            //Remove Rowspan
            //var indexOfColumnToRowSpan = 0;
            //var $table = $('#StatusTable');
            //var rowSpanMap = {};
            //$table.find('tr').each(function () {
            //    var valueOfTheSpannableCell = $($(this).children('td')[indexOfColumnToRowSpan]).text();
            //    $($(this).children('td')[indexOfColumnToRowSpan]).attr('data-original-value', valueOfTheSpannableCell);
            //    rowSpanMap[valueOfTheSpannableCell] = true;
            //});

            //for (var rowGroup in rowSpanMap) {
            //    var $cellsToSpan = $('td[data-original-value="' + rowGroup + '"]');
            //    var numberOfRowsToSpan = $cellsToSpan.length;
            //    $cellsToSpan.each(function (index) {
            //        if (index == 0) {
            //            $(this).attr('rowspan', numberOfRowsToSpan);
            //        } else {
            //            $(this).hide();
            //        }
            //    });
            //}
            //Remove Colspan
            //$table.each(function () {
            //    var cells = $('td', this);
            //    for (var i = 0; i < cells.length; i++) {
            //        debugger
            //        if ($(cells[i]).text() === '' && $(cells[i + 1]).text() === '') {
            //            var colSpan = parseInt($(cells[i]).attr('colspan'), 10) || 1;

            //            $(cells[i]).attr('colspan', ++colSpan);
            //            $(cells[i + 1]).remove();
            //            cells = $('td', this);
            //            i -= 1;
            //        }
            //    }
            //});
        }
    });
    $("#divViewRemarks").html(''); 
}
function getDDL(hidId, Status, menuid) {
    dataString = { MenuId: menuid }
    $.ajax({
        url: "../detailsGeneral.aspx/Status_Dropdown",
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(dataString),
        success: function (mydata) {
            var ddlrow = "<div>";
            ddlrow += "<select class='form-control dataMasterInputReport' id='txtSelectVal_" + hidId + "'>";
            ddlrow += "<option value=''>Select</option>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                $.each(jsondata, function (key, value) {
                    var a = "";
                    if (value.AutoId == Status) {
                        a = "selected";
                    }
                    ddlrow += "<option " + a + " value=" + value.AutoId + ">" + value.StatusName + "</option>";
                });
            }
            ddlrow += "</select>";
            ddlrow += "</div>";
            $("#ddlid_" + hidId + "").html(ddlrow);
        }
    });
}

//Get EL Details
function ViewELdetails(args) {
    debugger
    var Autoid = $(args).attr('Autoid');
    var Data = JSON.stringify({ Autoid: Autoid });
    $.ajax({
        url: "../detailsGeneral.aspx/ViewELdetails",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var jsondata = JSON.parse(mydata.d);
            $.each(jsondata, function (key, value) {
                $("#lblNotes").html(value.BDComments);
                if (value.BDComments != "") {
                    $("#lblNotes").html("Notes: " + value.BDComments);
                }
                getEngagementLetterAssignmentList(value.QuotationId);
            });
            $("#PopUpELDetails").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    });
}

function getEngagementLetterAssignmentList(id) {
    var Data = JSON.stringify({ Autoid: id });
    $.ajax({
        url: "../detailsGeneral.aspx/GetELAssignment",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = " <table class='table table-bordered'>";
            row += "<thead><tr style='background: #666; color: #fff'><th>Assignment</th><th>Type of Assignment</th><th>Period</th></tr></thead>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                row += "<tbody>";
                $.each(jsondata, function (key, value) {
                    row += "<tr><td>" + value.serviceName + "</td><td>" + value.TYPE_OF_ASSIGNMENT + "</td><td>" + value.PeriodFrom + " - " + value.PeriodTo + "</td></tr>";
                });
                row += "</tbody>";
            }
            row += "</table>";
            $("#divTypeofAssignment").html(row);
        }
    });
    $("#divTypeofAssignment").html('');
}
function ViewRequest(Autoid, MenuId) {
    var Data = JSON.stringify({ Autoid: Autoid, MenuId: MenuId });
    $.ajax({
        url: "../detailsGeneral.aspx/GetAllRequestList",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = " <table class='table table-bordered'>";
            row += "<thead><tr style='background: #666; color: #fff'><th>Type</th><th>By</th><th>On</th></tr></thead>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                row += "<tbody>";
                $.each(jsondata, function (key, value) {
                    row += "<tr><td>" + value.RequestName + "</td><td>" + value.CreatedByName + "</td><td>" + value.CreatedOn + "</td></tr>";
                });
                row += "</tbody>";
            }
            row += "</table>";
            $("#divViewRequest").html(row);
            $("#PopUpViewRequest").modal({
                backdrop: 'static',
                keyboard: false
            });
        }
    });
    $("#divViewRequest").html('');
}
function getFullCategory(EntityId, AssignmentId, Consultantid) {
    getEntityCategory(EntityId,"Client");
    getAssignmentCategory(AssignmentId,"Assignment");
    //getConsultantCategory(Consultantid);
    $("#PopUpEntityCategory").modal({
        backdrop: 'static',
        keyboard: false
    });
}
function getEntityCategory(Autoid, Heading) {
    var Data = JSON.stringify({ Autoid: Autoid, MenuId: 28, Type: "Entity" });
    $.ajax({
        url: "../detailsGeneral.aspx/ViewCategoryList",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = "<h4><strong><u>" + Heading +"</u></strong></h4>";
            row += " <table class='table table-bordered'>";
            row += "<thead><tr style='background: #666; color: #fff'><th>Type</th><th>Category</th><th>Remarks</th><th>By</th><th>On</th></tr></thead>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                row += "<tbody>";
                $.each(jsondata, function (key, value) {
                    row += "<tr><td>" + value.CategoryGroup + "</td><td>" + value.CategoryName + "</td><td>" + value.Remarks + "</td><td><span style='display: inline-block; width: 70px; background: #ddd; padding: 0px 4px'>Created:</span>" + value.CreatedByName + "<br><span style='display: inline-block; width: 70px; background: #ddd; padding: 0px 4px'>Updated:</span>" + value.UpdatedByName + "</td><td><span style='display: inline-block; width: 70px; background: #ddd; padding: 0px 4px'>Created:</span>" + value.CreatedOn + "<br><span style='display: inline-block; width: 70px; background: #ddd; padding: 0px 4px'>Updated:</span>" + value.UpdatedOn + "</td></tr>";
                });
                row += "</tbody>";
            }
            row += "</table>";
            debugger
            $("#divEntityCategory").html(row);
            $("#divCategoryEntityLogs").html(row);

        }
    });
    $("#divEntityCategory").html('');
    $("#divCategoryEntityLogs").html('');
}
function getAssignmentCategory(Autoid, Heading) {
    debugger
    var Data = JSON.stringify({ Autoid: Autoid, MenuId: 14, Type: "Assignment" });
    $.ajax({
        url: "../detailsGeneral.aspx/ViewCategoryList",
        data: Data,
        type: "POST",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (mydata) {
            var row = "<h4><strong><u>" + Heading +"</u></strong></h4>";
            row += " <table class='table table-bordered'>";
            row += "<thead><tr style='background: #666; color: #fff'><th>Type</th><th>Category</th><th>Remarks</th><th>By</th><th>On</th></tr></thead>";
            if (mydata.d != "") {
                var jsondata = JSON.parse(mydata.d);
                row += "<tbody>";
                $.each(jsondata, function (key, value) {
                    row += "<tr><td>" + value.CategoryGroup + "</td><td>" + value.CategoryName + "</td><td>" + value.Remarks + "</td><td><span style='display: inline-block; width: 70px; background: #ddd; padding: 0px 4px'>Created:</span>" + value.CreatedByName + "<br><span style='display: inline-block; width: 70px; background: #ddd; padding: 0px 4px'>Updated:</span>" + value.UpdatedByName + "</td><td><span style='display: inline-block; width: 70px; background: #ddd; padding: 0px 4px'>Created:</span>" + value.CreatedOn + "<br><span style='display: inline-block; width: 70px; background: #ddd; padding: 0px 4px'>Updated:</span>" + value.UpdatedOn + "</td></tr>";
                });
                row += "</tbody>";
            }
            row += "</table>";
            $("#divAssignmentCategory").html(row);
            $("#divAssignmentCategoryLogs").html(row);

        }
    });
    $("#divAssignmentCategory").html('');
    $("#divAssignmentCategoryLogs").html('');
}