$(document).ready(function () {

    $('#Country').change(function(){
        loadState($(this));
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
        PopulateDropDown("#state", data);
    });
}
function PopulateDropDown(dropDownId, list, selectedId) {
    $(dropDownId).empty();
    $(dropDownId).append("<option>--SELECT STATE--</option>")
    $.each(list, function (index, row) {
        $(dropDownId).append("<option value='" + row.stateId + "'>" + row.stateName + "</option>")
    });
}