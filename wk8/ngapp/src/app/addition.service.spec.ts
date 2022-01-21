import { AdditionService } from './addition.service';

describe('AdditionService', () => {
  let service: AdditionService;

  it('should be created', () => {
    service = new AdditionService();
    expect(service).toBeTruthy();
  });

  it('should add 2 and 2', () => {
    service = new AdditionService();
    let result = service.add(2, 2);
    expect(result).toBe(4);
  });
});
