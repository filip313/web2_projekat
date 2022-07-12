import { PorudzbinaProizvod } from "./porudzbinaproizvod.model";

export class NovaPorudzbina{
        id:number;
        adresa:string = "";
        komentar:string = "";
        cena:number;
        proizvodi:PorudzbinaProizvod[];
        narucilacId:number;
}