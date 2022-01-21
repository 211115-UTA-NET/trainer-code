import { Component, Input } from '@angular/core';
import { TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { AppComponent } from './app.component';
import { ChildComponent } from './child/child.component';

describe('AppComponent', () => {
  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule],
      declarations: [AppComponent, FakeChildComponent],
    }).compileComponents();
  });

  it('should create the app', () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app).toBeTruthy();
  });

  it(`should have as title 'ngapp'`, () => {
    const fixture = TestBed.createComponent(AppComponent);
    const app = fixture.componentInstance;
    expect(app.title).toEqual('ngapp');
  });

  it('should render title', () => {
    const fixture = TestBed.createComponent(AppComponent);
    // in the test environment, angular's "change detection cycle" does not run automatically
    // you control when it runs - by calling "fixture.detectChanges"
    fixture.detectChanges();
    const compiled = fixture.nativeElement as HTMLElement;
    expect(compiled.querySelector('.content span')?.textContent).toContain(
      'ngapp app is running!'
    );
  });

  it('should pass title to child', () => {
    const title = 'asdf';
    const fixture = TestBed.createComponent(AppComponent);
    fixture.componentInstance.title = title;
    fixture.detectChanges();
    const childComponent = fixture.debugElement.query(
      By.directive(FakeChildComponent)
    );
    // accessing properties of the child component (componentInstance can't see them for some reason)
    expect(childComponent.context.data).toBe(title);
  });
});

@Component({ selector: 'app-child' })
class FakeChildComponent {
  @Input() data!: string;
}
