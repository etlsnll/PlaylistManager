import { Component, Inject } from '@angular/core';
import { Http, URLSearchParams } from '@angular/http';

@Component({
    selector: 'app-playlists',
    templateUrl: './playlists.component.html',
    styleUrls: ['./playlists.component.css']
})
/** playlists component*/
export class PlaylistsComponent {

    public playlists: PlaylistSummary[] = [];
    public totalPlaylists: number = 0;
    public p: number = 1;
    public pageSize: number = 10;
    public pageSizes: number[] = [10, 20, 30, 40, 50];
    public totalPages: number = 0;
    private url: string;
    private http: Http;

    /** playlists ctor */
    constructor(http: Http, @Inject('BASE_URL') baseUrl: string) {
        this.url = baseUrl;
        this.http = http;
        this.getPlaylists();
    }

    public newPage(page: number) {
        this.p = page;
        this.getPlaylists();
    }

    public changePageSize(size: number) {
        this.p = 1; // reset to first page
        this.pageSize = size;
        this.getPlaylists();
    }

    //TODO - factor this out into a service when working:
    private getPlaylists() {

        this.http.get(this.url + 'api/MusicLibrary/CountAllPlaylists')
                .subscribe(result => {
                    this.totalPlaylists = result.json() as number;
                    this.totalPages = Math.floor(this.totalPlaylists / this.pageSize) + 1;
                }, error => console.error(error));

        var search = new URLSearchParams();
        search.set('pageNum', this.p.toString()); // Add URL query param
        search.set('pageSize', this.pageSize.toString()); // Add URL query param
        this.http.get(this.url + 'api/MusicLibrary/AllPlaylists', { search: search })
                .subscribe(result => {
                    this.playlists = result.json() as PlaylistSummary[];
                }, error => console.error(error));
    }
}

interface PlaylistSummary {
    id: number;
    name: string;
    numTracks: number;
}