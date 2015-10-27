$(document).ready(function () {


    $("#dialog").dialog();

    //VAZNO MNOGU - vo grupite da se trga
    $("input[type='radio']").each(function (index) {
        var ime = $(this).attr("name").split("$");
        $(this).attr("name", ime[1])

    });



    $("div[Grupa]").children("#Sodrzina").hide();

    $("div[Grupa]").each(function (index) {
        if ($(this).attr("Prikazi") == "DA") {

            $(this).children("#Sodrzina").show();
        }
    })

    $("div[id='Kontrola']").each(function (index) {
        if ($(this).attr("Prikazi") != "DA") {

            $(this).hide();
        }
    })

    $("#NaslovGrupa").live('click', function () {


        if (!$(this).parent("div").children("#Sodrzina").is(':visible')) {
            $(this).parent("div").children("#Sodrzina").show();
        }
        else {
            $(this).parent("div").children("#Sodrzina").hide();
        }

    });
    // Kraj za grupa

    //Za Datum na focus out da pravi proverka
    $("#DatumPicker input[type='text']").live('focusout', function () {

        proverkaDatum(event, this, 'FOUT');
    })
});

//Vazno za RadioButton
function setirajRB(ova) {
    var grupa = $(ova).attr("GrupaRadio");

    jQuery.each($('[GrupaRadio="' + grupa + '"]'), function () {

        $(this).find("input[type='hidden']").val("")
    });

    $(ova).find("input[type='hidden']").val("D");
    $(ova).find("input[type='radio']").attr("checked", true);

}

function setirajCB(ova) {

    if ($(ova).find("input[type='hidden']").val() != null) {

        if ($(ova).find("input[type='hidden']").val() == "D") {
            $(ova).find("input[type='hidden']").val("");
            $(ova).find("input[type='checkbox']").attr("checked", false);
        }
        else {
            $(ova).find("input[type='hidden']").val("D");
            $(ova).find("input[type='checkbox']").attr("checked", true);
        }
    }
    
}
