import { Time } from "@angular/common";
import { PorudzbinaProizvod } from "./porudzbinaproizvod.model";
import { UserData } from "./user.model";

export class Porudzbina{
        id:number;
        adresa:string = "";
        komentar:string = "";
        cena:number;
        trajanjeDostave:any;
        vremePrihvata:Time;
        status:string="";
        Proizvodi: PorudzbinaProizvod[];
        narucilac:UserData;
        dostavljac:UserData;
}