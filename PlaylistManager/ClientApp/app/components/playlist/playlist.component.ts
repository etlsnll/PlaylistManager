import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { Playlist } from '../../playlist'; // Import the form model
import { PlaylistService } from '../shared/playlist.service';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
    selector: 'app-playlist',
    templateUrl: './playlist.component.html',
    styleUrls: ['./playlist.component.css'],
    providers: [PlaylistService]
})
/** playlist component*/
export class PlaylistComponent implements OnInit {

    model = new Playlist("");
    id: number = 0;

    /** playlist ctor */
    constructor(private playlistService: PlaylistService,
        private route: ActivatedRoute) {

        //TODO: get list of playlist tracks..

        this.model.name = this.id.toString(); // TODO - this.userService.getUser(this.id);
    }

    ngOnInit() {
        this.route.params
            .subscribe(
                (params: Params) => {
                    this.id = params['id'];
                }
            )
    }

    onSubmit(): void {
        //TODO
        console.log("TODO: update playlist name: " + this.model.name + ", id: " + this.id);
    }
}