import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { PlaylistService, PlaylistDetails, Track } from '../shared/playlist.service';
import { ActivatedRoute, Params } from '@angular/router';

@Component({
    selector: 'app-playlist',
    templateUrl: './playlist.component.html',
    styleUrls: ['./playlist.component.css'],
    providers: [PlaylistService]
})
/** playlist component*/
export class PlaylistComponent implements OnInit {

    model = new PlaylistDetails(0, "Unknown", 0, new Array<Track>());
    id: number = 0;

    /** playlist ctor */
    constructor(private playlistService: PlaylistService,
                private route: ActivatedRoute) { }

    ngOnInit() {
        // Get ID from param in URL:
        this.route.params
            .subscribe((params: Params) => {
                this.id = params['id'];
                this.playlistService.getPlaylist(this.id).subscribe(pl => this.model = pl);
            })
    }

    onSubmit(): void {
        //TODO
        //console.log("TODO: update playlist name: " + this.model.name + ", id: " + this.id);
    }
}
