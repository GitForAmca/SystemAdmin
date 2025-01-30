
function selectCheckToDelete(senderid, icount, strchkbox) {
    var flag = false;
    for (var i = 1; i <= icount; i++) {
        if (document.getElementById(senderid + strgetid(i) + strchkbox).checked) {
            flag = true;
            break;
        }
    }
    if (flag == false) {
        alert('You must check at least one of the record to delete !');
        return false;
    }
    else {
        if (confirm('Are you sure to delete this record !')) {
            return true;
        }
        else {
            return false;
        }
    }
}

function selectCheckToEdit(senderid, icount, strchkbox) {
    debugger;
    var count = 0;
    var flag = false;
    for (var ii = 1; ii <= icount; ii++) {
        if (document.getElementById(senderid + strgetid(ii) + strchkbox).checked) {
            count += 1;
        }
    }
    if (count > 1) {
        alert('You must check only one record to edit !');
        return false;
    }
    for (var i = 1; i <= icount; i++) {
        if (document.getElementById(senderid + strgetid(i) + strchkbox).checked) {
            flag = true;
            break;
        }
    }
    if (flag == false) {
        alert('You must check any one of the record to edit !');
        return false;
    }
    else {
        return true;
    }
}
function selectCheckToMulEdit(senderid, icount, strchkbox) {
    var count = 0;
    var flag = false;
    for (var ii = 1; ii <= icount; ii++) {
        if (document.getElementById(senderid + strgetid(ii) + strchkbox).checked) {
            count += 1;
        }
    }
    for (var i = 1; i <= icount; i++) {
        if (document.getElementById(senderid + strgetid(i) + strchkbox).checked) {
            flag = true;
            break;
        }
    }
    if (flag == false) {
        alert('You must check any one of the record to edit !');
        return false;
    }
    else {
        return true;
    }
}
function selectCheckToEditAll(senderid, icount, strchkbox) {
    var count = 0;
    var flag = false;
    for (var i = 1; i <= icount; i++) {
        if (document.getElementById(senderid + strgetid(i) + strchkbox).checked) {
            flag = true;
            break;
        }
    }
    if (flag == false) {
        alert('Please select atleast one record to edit !');
        return false;
    }
    else {
        return true;
    }
}

function selectToAction(senderid, icount, strchkbox, strAction) {
    var count = 0;
    var flag = false;
    for (var ii = 0; ii < icount; ii++) {
        if (document.getElementById(senderid + strgetid(ii) + strchkbox).checked) {
            count += 1;
        }
    }
    if (count > 1) {
        alert('Please select only one record to  ' + strAction + ' !');
        return false;
    }
    for (var i = 0; i < icount; i++) {
        if (document.getElementById(senderid + strgetid(i) + strchkbox).checked) {
            flag = true;
            break;
        }
    }
    if (flag == false) {
        alert('Please select one record to  ' + strAction + ' !');
        return false;
    }
    else {
        return true;
    }
}
function selectAllToAction(senderid, icount, strchkbox, strAction) {
    var count = 0;
    var flag = false;
    for (var i = 1; i <= icount; i++) {
        if (document.getElementById(senderid + strgetid(i) + strchkbox).checked) {
            flag = true;
            break;
        }
    }
    if (flag == false) {
        alert('Please select atleast one record to ' + strAction + ' !');
        return false;
    }
    else {
        return true;
    }
}
function selectAll(senderid, icount, strchkbox, isselect) {
    for (var i = 1; i <= icount; i++) {
        if (document.getElementById(senderid + strgetid(i) + strchkbox).disabled == false)
            document.getElementById(senderid + strgetid(i) + strchkbox).checked = isselect;
    }
    return false;
}
function selectDeselectAll(sender, senderid, icount, strchkbox) {
    for (var i = 1; i <= icount; i++) {
        document.getElementById(senderid + strgetid(i) + strchkbox).checked = sender.checked;
    }
}
function selectAllLv(senderid, icount, strchkbox, isselect) {
    for (var i = 0; i < icount; i++) {
        if (document.getElementById(senderid + i.toString() + strchkbox).disabled == false)
            document.getElementById(senderid + i.toString() + strchkbox).checked = isselect;
    }
    return false;
}
function selectDeselectAllLv(sender, senderid, icount, strchkbox) {
    for (var i = 0; i < icount; i++) {
        document.getElementById(senderid + i.toString() + strchkbox).checked = sender.checked;
    }
}
function selectCheckToEditLv(senderid, icount, strchkbox) {
    var count = 0;
  //  alert(senderid + strchkbox + "_" + icount.toString())
    var flag = false;
    for (var ii = 0; ii < icount; ii++) {
        if (document.getElementById(senderid + strchkbox + "_" + ii.toString()).checked) {
            count += 1;
        }
    }
    if (count > 1) {
        alert('Please select only one record to edit !');
        return false;
    }
    for (var i = 0; i < icount; i++) {
        if (document.getElementById(senderid + strchkbox + "_" + i.toString()).checked) {
            flag = true;
            break;
        }
    }
    if (flag == false) {
        alert('Please select one record to edit !');
        return false;
    }
    else {
        return true;
    }
}



function selectCheckToEditAllLv(senderid, icount, strchkbox) {

    //alert(senderid + strchkbox + "_" + icount.toString())
    var count = 0;
    var flag = false;
    for (var i = 0; i < icount; i++) {
        if (document.getElementById(senderid +  strchkbox+"_"+i.toString()).checked) {
            flag = true;
            break;
        }
    }
    if (flag == false) {
        alert('Please select atleast one record to edit !');
        return false;
    }
    else {
        return true;
    }
}

function selectToActionLv(senderid, icount, strchkbox, strAction) {
    var count = 0;
    var flag = false;
    for (var ii = 0; ii < icount; ii++) {
        if (document.getElementById(senderid +  strchkbox + "_" + ii.toString()).checked) {
            count += 1;
        }
    }
    if (count > 1) {
        alert('Please select only one record to  ' + strAction + ' !');
        return false;
    }
    for (var i = 0; i < icount; i++) {
        if (document.getElementById(senderid + strchkbox + "_" + i.toString()).checked) {
            flag = true;
            break;
        }
    }
    if (flag == false) {
        alert('Please select one record to  ' + strAction + ' !');
        return false;
    }
    else {
        return true;
    }
}

function selectAllToActionLv(senderid, icount, strchkbox, strAction) {
    var count = 0;
    var flag = false;
    for (var i = 0; i < icount; i++) {
        if (document.getElementById(senderid + strchkbox + "_" + i.toString()).checked) {
            flag = true;
            break;
        }
    }
    if (flag == false) {
        alert('Please select atleast one record to ' + strAction + ' !');
        return false;
    }
    else {
        return true;
    }
}
function selectCheckToDeleteLv(senderid, icount, strchkbox) {
    var flag = false;
    for (var i = 0; i < icount; i++) {
        if (document.getElementById(senderid + strchkbox + "_" + i.toString()).checked) {
            flag = true;
            break;
        }
    }
    if (flag == false) {
        alert('Please select at least one of the record to delete !');
        return false;
    }
    else {
        if (confirm('Are you sure to delete this record !')) {
            return true;
        }
        else {
            return false;
        }
    }
}
function strgetid(strid) {
    if (Number(strid) <= 9)
        return '0' + strid.toString();
    return strid.toString();
}

function CheckWeightage(strid, strVal) {
    if (document.getElementById(strid).value > Number(strVal)) {
        document.getElementById(strid).value = "";
        alert("Weightage can't be greater than " + strVal + " %.");
        return false;
    }
}

function ConfirmDeleteAllFiles() {
    if (confirm('Are you sure to delete all files !')) {
        return true;
    }
    else {
        return false;
    }
}
function CheckDecimal(e) {
        var key //= (window.event) ? event.keyCode : e.which;
        if (window.event) {
            key = event.keyCode
        }
        else {
            key = e.which
        }
        // Was key that was pressed a numeric character (0-9) and (.) or backspace (8)?
        if (key > 47 && key < 58 || key == 46 || key == 8 || key == 0)
            return; // if so, do nothing
        else // otherwise, discard character
            if (window.event) //IE
        {
            window.event.returnValue = null;
        } else //Firefox
        {
            e.preventDefault();
        }
    }