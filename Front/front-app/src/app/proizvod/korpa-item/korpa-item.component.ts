import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-korpa-item',
  templateUrl: './korpa-item.component.html',
  styleUrls: ['./korpa-item.component.css']
})
export class KorpaItemComponent implements OnInit {

  constructor(public dialogRef:MatDialogRef<KorpaItemComponent>,
    @Inject(MAT_DIALOG_DATA) public data:number ) { }

  ngOnInit(): void {
  }

  onNoClick(){
    this.dialogRef.close();
  }
}
