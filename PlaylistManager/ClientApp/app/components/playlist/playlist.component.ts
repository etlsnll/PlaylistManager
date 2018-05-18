import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { FormControl } from '@angular/forms';
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
    tracks: Track[] = [];
    searchTitle: FormControl = new FormControl();
    searchArtist: FormControl = new FormControl();
    searchAlbum: FormControl = new FormControl();

    /** playlist ctor */
    constructor(private playlistService: PlaylistService,
                private route: ActivatedRoute) {
        // Assign actions to search fields, using debounce delay to limit search requests to web API at server until user finishes typing:
        this.searchTitle.valueChanges
            .debounceTime(400)
            .subscribe(data => this.searchTracks());
        this.searchArtist.valueChanges
            .debounceTime(400)
            .subscribe(data => this.searchTracks());
        this.searchAlbum.valueChanges
            .debounceTime(400)
            .subscribe(data => this.searchTracks());
    }

    ngOnInit() {
        // Get ID from param in URL:
        this.route.params
            .subscribe((params: Params) => {
                this.id = params['id'];
                this.playlistService.getPlaylist(this.id).subscribe(pl => {
                    this.model = pl;
                });  
            })
    }

    searchTracks() {
        //console.log(this.searchTitle.value + ", " + this.searchArtist.value + ", " + this.searchAlbum.value);
        var title: string = (this.searchTitle.value !== null ? this.searchTitle.value.toString() : "");
        var artist: string = (this.searchArtist.value !== null ? this.searchArtist.value.toString() : "");
        var album: string = (this.searchAlbum.value !== null ? this.searchAlbum.value.toString() : "");

        if (title === "" && artist === "" && album === "") {
            this.clearResults();
        }
        else {
            this.playlistService.searchTracks(title, artist, album)
                                .subscribe(result => this.tracks = result);
        }
    }

    clearResults() {
        this.tracks = new Array<Track>();
    }

    // Method to update playlist name:
    onSubmit(): void {
        //TODO
        //console.log("TODO: update playlist name: " + this.model.name + ", id: " + this.id);
    }
}
