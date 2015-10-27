//--- Klasi za objekti od baza ---//
var tipNasiObj = new Array();
tipNasiObj[0]="Artikal";
tipNasiObj[1] = "Komintent";



////// Nas objekt - osnoven
function NasObj(podatoci) {

    this.TipNaObj = function(){
   
        if (podatoci.TipObjekt.toString().toUpperCase() == tipNasiObj[0].toUpperCase())
            return 0;
        else if (podatoci.TipObjekt.toString().toUpperCase() == tipNasiObj[1].toUpperCase())
            return 1;
        else
            return null;
  };

  this.VratiObjekt = function () {

      var brTipObj = this.TipNaObj();



      if (brTipObj == 0) {
          var pom = new Artikal(podatoci.Objekt);
          return pom;
      }
      else if (brTipObj == 1) {
          var pom = new Komintent(podatoci.Objekt);
          return pom;
      }
      else {
          return null;
      }

  };

}
////////
function Artikal(art) {

    this.Sifra_Art = art.Sifra_Art;
    this.ImeArt = art.ImeArt;

    this.ZemiVrednost = function () {
        return this.Sifra_Art;
    };

    this.ZemiIme = function () {
        return this.ImeArt;
    };
}

function Komintent(komint) {


    this.Sifra_Kup = komint.Sifra_Kup;
    this.ImeKup = komint.ImeKup;

    this.ZemiVrednost = function () {
        return this.Sifra_Kup;
    };

    this.ZemiIme = function () {
        return this.ImeKup;
    };
}