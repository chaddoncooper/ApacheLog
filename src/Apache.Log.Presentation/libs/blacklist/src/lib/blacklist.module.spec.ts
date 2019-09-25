import { async, TestBed } from '@angular/core/testing';
import { BlacklistModule } from './blacklist.module';

describe('BlacklistModule', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [BlacklistModule]
    }).compileComponents();
  }));

  it('should create', () => {
    expect(BlacklistModule).toBeDefined();
  });
});
