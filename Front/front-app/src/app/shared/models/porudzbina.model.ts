import { Time } from "@angular/common";
import { PorudzbinaProizvod } from "./porudzbinaproizvod.model";
import { UserData } from "./user.model";

export class Porudzbina{
        id:number;
        adresa:string = "";
        komentar:string = "";
        cena:number;
        trajanjeDostave:any;
        vremePrihvata:Date;
        status:string="";
        proizvodi: PorudzbinaProizvod[];
        narucilac:UserData;
        dostavljac:UserData;
}