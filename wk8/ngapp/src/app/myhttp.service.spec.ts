import { HttpClient, HttpClientModule } from '@angular/common/http';
import { TestBed } from '@angular/core/testing';
import { of } from 'rxjs';

import { MyhttpService } from './myhttp.service';
import { User } from './user';

describe('MyhttpService', () => {
  let service: MyhttpService;
  let fakeHttp: any;

  // two ways to test services in angular...
  // 1. just make the service yourself ("new"), plus any dependencies
  // 2. set up a testing NgModule, and ask it to make the service for you.

  beforeEach(() => {
    fakeHttp = {
      get() { }
    };

    TestBed.configureTestingModule({
      // imports: [HttpClientModule] // <-- wrong - don't use the real httpclient for a unit test
      providers: [{ provide: HttpClient, useValue: fakeHttp }], // <-- registering a fake httpclient for dependency injection
    });

    service = TestBed.inject(MyhttpService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should receive list of users', (done: DoneFn) => {
    let expectedUrl = 'https://jsonplaceholder.typicode.com/users';

    // set up the dependency to return a reasonable observable

    // the "of" function creates an observable that immediately resolves with a given value
    spyOn(fakeHttp, 'get').and.returnValue(of([]));

    service.getUsers().then((users) => {
      expect(users.length).toBe(0);
      expect(fakeHttp.get).toHaveBeenCalledWith(expectedUrl);
      done();
    });
  });
});
