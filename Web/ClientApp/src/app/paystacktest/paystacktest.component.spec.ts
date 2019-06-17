import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PaystacktestComponent } from './paystacktest.component';

describe('PaystacktestComponent', () => {
  let component: PaystacktestComponent;
  let fixture: ComponentFixture<PaystacktestComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PaystacktestComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PaystacktestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
