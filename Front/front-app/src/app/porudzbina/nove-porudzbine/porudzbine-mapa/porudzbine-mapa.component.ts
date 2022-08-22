import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { PorudzbinaService } from 'src/app/shared/services/porudzbina.service';
import OlMap from 'ol/Map';
import View from 'ol/View';
import VectorLayer from 'ol/layer/Vector';
import Style from 'ol/style/Style';
import Icon from 'ol/style/Icon';
import OSM from 'ol/source/OSM';
import * as olProj from 'ol/proj';
import TileLayer from 'ol/layer/Tile';
import VectorSource from 'ol/source/Vector';
import { Feature } from 'ol';
import { Point } from 'ol/geom';
import { FeatureLike } from 'ol/Feature';
import { GeocodingService } from 'src/app/shared/services/geocoding.service';
import { Porudzbina } from 'src/app/shared/models/porudzbina.model';
import { PrihvatiPorudzbinu } from 'src/app/shared/models/prihavtiporudzbinu.model';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-porudzbine-mapa',
  templateUrl: './porudzbine-mapa.component.html',
  styleUrls: ['./porudzbine-mapa.component.css']
})
export class PorudzbineMapaComponent implements OnInit {

  constructor(private snackBar:MatSnackBar, private service: PorudzbinaService,
    private router:Router, private geoService: GeocodingService, private auth: AuthService) { }

  ngOnInit(): void {
    this.map = new OlMap({
      target: 'mapa-div',
      layers: [
        new TileLayer({
          source: new OSM(),
        }),
      ],
      view: new View({
        center: olProj.fromLonLat([19.834833045756262, 45.254221659981575]),
        zoom: 15,
      }),
    });

    let markeri_vektor = new VectorLayer({
      source: new VectorSource(),
      style: new Style({
        image: new Icon({
          anchor: [0.5, 1],
          src:'../../../assets/pin4.png',
        })
      })
    })
    this.map.addLayer(markeri_vektor);
      this.service.getNovePorudzbine().subscribe(
        (data) => {
          data.forEach((value, index) => {
            let projetion = this.map.getView().getProjection();
            let source = markeri_vektor.getSource();
            this.geoService.getCoords(value.adresa).subscribe(
              (data) => {
                console.log(data);
                let temp_feature = new Feature(new Point(
                  olProj.fromLonLat([data.results[0].lon, data.results[0].lat], projetion)));
                this.markeri.set(temp_feature, value);
                source?.addFeature(temp_feature);
              },
              (error) => {
                console.log(error);
              }
            )
          })
        },
        (error) => {
          this.router.navigateByUrl('user/login');
        });

    this.map.on('click', (event) => {
      let feature = this.map.forEachFeatureAtPixel(event.pixel, (feature, layer) =>{
        let coords = event.coordinate;
        return feature;
      })

      if(feature != null && feature !== undefined){
        if(this.markeri.has(feature)){
          this.izabranaPorudzbina = this.markeri.get(feature);
          console.log(this.izabranaPorudzbina);
        }
      }
      else{
        this.izabranaPorudzbina = null;
      } 
    })
  }

  prihvatiPorudzbinu(){
    let data = new PrihvatiPorudzbinu();
    data.dostavljacId = this.auth.getUserId();
    data.porudzbinaId = this.izabranaPorudzbina.id;
    this.service.prihvatiPorudzbinu(data).subscribe(
      (data) => {
        this.router.navigateByUrl('/porudzbina/trenutna');
        this.snackBar.open("Porudzbina uspesno prihvacena", "", {duration: 2000});
      },
      (error) => {
        this.snackBar.open(error.error, "", {duration: 2000});
      }
    );
  }

  map:OlMap;
  markeri = new Map<FeatureLike, Porudzbina>();
  izabranaPorudzbina :any= null;
}
