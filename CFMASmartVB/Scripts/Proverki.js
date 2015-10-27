function DaliSifra(sifra) 
{
    if (!isNaN(sifra)) {
        return true;
    }
    else 
    return false
}
function DaliBarKod(barKod) {

    var pom = barKod.substring(0, 1);

    if (pom == "+") {

        var dol = barKod.length-1;
        var pom1 = barKod.substring(1, dol);
        if (!isNaN(pom1)) {
            return true;
        }
        else
            return false;
    }
    else
        return false;
}

function proverkaDatum(e, koj, tip) {
     
     if (proveriDaliNavgiacija(dajmiKeyKod(e))) {
         return true;
     }
        

    $(koj).val(dozvoliSamoBroevi(e, $(koj).val()));


    var tekst = $(koj).val();
    var daliPrestapna = true;
    var tatko = $(koj).closest("#DatumPicker");

    var godinaObj= $(tatko).find("[nasID='Godina']");
    var mesecObj=$(tatko).find("[nasID='Mesec']");
    var denObj = $(tatko).find("[nasID='Den']");
   
    var godina = $(godinaObj).val();
    var mesec = $(mesecObj).val();
    var den = $(denObj).val();

    var d = new Date();

    var tekDen = d.getDay();
    var tekGodina =d.getFullYear();
    var tekMesec  = d.getMonth();


    if (!isNaN(tekst)) {
        if (godina < 2000 || godina > 3000) {
            //Treba da ja prikazi tekovnata godina
           if (tip == "FOUT") {
                $(godinaObj).val(tekGodina);
                proverkaDatum(godinaObj, 'FOUT');
            }
            return false;
        }

        if (mesec < 1 || mesec > 12) {
            //Treba da se prikazi tekoven mesec
           //if(tip=="FOUT") {

                //$(mesecObj).val(tekMesec);
                if (mesec < 1) {
                    $(mesecObj).val('1');
                }
                else if (mesec >12){
                    $(mesecObj).val('12');
                }
                proverkaDatum(mesecObj, 'FOUT');
            //}
        }

        if (den < 1 || den > proverkaMesecZaDen(mesec, godina)) {

            //if (tip == "FOUT") {

                if(den < 1) {

                     $(denObj).val('1');
                }
                else {
                
                     $(denObj).val(proverkaMesecZaDen(mesec, godina));
                }             
                proverkaDatum(denObj, 'FOUT');
          //  }
            return false;
        }

    }

    else {
        $(koj).select(); 
        $(koj).focus();
        if (tip == "FOUT") {

        }
        return false;
    }

    
    
}

function isleap(godina) {
    var yr = godina;
    if ((parseInt(yr) % 4) == 0) {
        if (parseInt(yr) % 100 == 0) {
            if (parseInt(yr) % 400 != 0) {

                return false;
            }
            if (parseInt(yr) % 400 == 0) {

                return "true";
            }
        }
        if (parseInt(yr) % 100 != 0) {

            return true;
        }
    }
    if ((parseInt(yr) % 4) != 0) {

        return false;
    }
}
function proverkaMesecZaDen(mesec,godina) {

    if (mesec == 1 ||
          mesec == 3 ||
          mesec == 5 ||
          mesec == 7 ||
          mesec == 8 ||
          mesec == 10 ||
          mesec == 12) {

        return 31;
    }
    else if (mesec == 2 && isleap(godina)) {

        return 29;
    }
    else if (mesec == 2) {

        return 28;
    }
    else
        return 30;
   

}

//Golemina na ekran
function ekran() {
    var elem = (document.compatMode === "CSS1Compat") ? document.documentElement : document.body;

    var height = elem.clientHeight;
    var width = elem.clientWidth;

    alert(width);
}
var vrednostSamoBroevi = "";
function dozvoliSamoBroevi(e, tekst) {
    if (isNaN(tekst)) {
        return "";
    }
    return tekst;

}

function proveriDaliNavgiacija(KeyKod) {

    if (KeyKod > 36 && KeyKod < 41 || KeyKod==8)
        return true;

    return false;
}

function dajmiKeyKod(e) {
    var keyCode = e.charCode ? e.charCode : e.keyCode ? e.keyCode : 0;
    var pom = parseInt(keyCode);

    return pom;
}
function proveriSUBMIT() {
    return false;
}