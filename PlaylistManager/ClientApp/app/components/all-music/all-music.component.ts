import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';

@Component({
    selector: 'all-music',
    templateUrl: './all-music.component.html',
    styleUrls: ['./all-music.component.css']
})

export class AllMusicComponent {
    public tracks: Track[];
    public totalTracks: number;
    public p: number = 1;
    public pageSize: number = 10;
    public pageSizes: number[] = [10, 20, 30, 40, 50];
    public totalPages: number;
    private url: string;
    private http: Http;

    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.url = baseUrl;
        this.http = http;
        this.getTracks();

        http.get(baseUrl + 'api/MusicLibrary/CountAllTracks').subscribe(result => {
            this.totalTracks = result.json() as number;
            this.totalPages = Math.floor(this.totalTracks / this.pageSize);
        }, error => console.error(error));

    }

    public newPage(page: number) {
        this.p = page;
        this.getTracks();
    }

    public changePageSize(size: number) {
        this.p = 1; // reset to first page
        this.pageSize = size;
        this.getTracks();
    }

    private getTracks() {
        this.http.get(this.url + 'api/MusicLibrary/AllTracks?pageNum=' + this.p + '&pageSize=' + this.pageSize).subscribe(result => {
            this.tracks = result.json() as Track[];
        }, error => console.error(error));
    }
}

interface Track {
    trackId: number;
    album: string;
    albumArtist: string | null;
    artist: string;
    year: number | null;
    title: string;
    trackNum: number | null;
}