import { Component, Input, EventEmitter, Output } from '@angular/core';

@Component({
    selector: 'app-pager',
    templateUrl: './pager.component.html',
    styleUrls: ['./pager.component.css']
})
/** pager component*/
export class PagerComponent {

    /** pager ctor */
    constructor() {
    }

    @Input()
    private page: number = 1;

    @Input()
    private totalPages: number = 0;

    @Input()
    private pageSize: number = 10;

    @Input()
    private pageSizes: number[] = [10,20,30,40,50,60,70,80,90,100];

    @Output()
    private changePage: EventEmitter<number> = new EventEmitter<number>();

    next() {
        this.changePage.emit(this.page < this.totalPages ? this.page + 1 : this.page);
    }

    prev() {
        this.changePage.emit(this.page > 1 ? this.page - 1 : this.page);
    }


    @Output()
    private newPageSize: EventEmitter<number> = new EventEmitter<number>();

    changePageSize(newSize: number) {
        this.pageSize = newSize;
        this.newPageSize.emit(newSize);
    }
}