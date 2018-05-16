import { Component } from '@angular/core';
import { Playlist } from '../../playlist'; // Import the form model

@Component({
    selector: 'app-add-playlist',
    templateUrl: './add-playlist.component.html',
    styleUrls: ['./add-playlist.component.css']
})
/** add-playlist component*/
export class AddPlaylistComponent {

    /** add-playlist ctor */
    constructor() { }

    model = new Playlist("");
    submitted = false;

    // Method to handle the form submission
    onSubmit() {
        this.submitted = true;
        console.log("added new playlist: " + this.model.name);
        // TODO
    }
}