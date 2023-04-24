$(document).ready(function () {

    $('#CountryId').change(function(){
        loadState($(this));
    });
    $('#StateId').change(function(){
        loadPinCode($(this));
    });    
    $("#btnDeleteImage").click(function () {
        var id = $(this).attr("data-id");
        $.ajax({
            url: '/User/DeleteImage/' + id,
            type: "POST",
            async: true,
            success: function (data) {
                if (data.succeeded) {
                    $("#delete-image-main").hide();
                    $("#ProfilePictureUrl").val("");
                }
                else {
                   // toastr.error(data.message);
                }
            },
            beforeSend: function () {
                $(this).attr("disabled", true);
                $(this).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Loading...');
            },
            complete: function () {
                $(this).html('Delete Image');
            },
        });
    });
});
function loadState(obj) {
    var value = obj.value;
    $.post("GetStatesByCountryId", { countryId: value }, function (data) {
        PopulateDropDown("#StateId", data, "<option>--SELECT STATE--</option>");
    });
}
function loadPinCode(obj) {
    var value = obj.value;
    $.post("GetPinCodesByStateId", { stateId: value }, function (data) {
        PopulatePinCodeDropDown("#PinCodeId", data, "<option>--SELECT PINCODE--</option>");
    });
}
function PopulatePinCodeDropDown(dropDownId, list, option) {
    $(dropDownId).empty();
    $(dropDownId).append(option)
    $.each(list, function (index, row) {
        $(dropDownId).append("<option value='" + row.pinCodeId + "'>" + row.name + "</option>")
    });
}
function PopulateDropDown(dropDownId, list, option) {
    $(dropDownId).empty();
    $(dropDownId).append(option)
    $.each(list, function (index, row) {
        $(dropDownId).append("<option value='" + row.stateId + "'>" + row.name + "</option>")
    });
}