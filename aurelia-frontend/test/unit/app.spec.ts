import {bootstrap} from 'aurelia-bootstrapper';
import {StageComponent, ComponentTester} from 'aurelia-testing';
import {PLATFORM} from 'aurelia-pal';

describe('Stage App Component', () => {
  let component = new ComponentTester<any>();

  beforeEach(() => {
    component = StageComponent
      .withResources(PLATFORM.moduleName('app'))
      .inView('<app></app>');
  });

  afterEach(() => component.dispose());

  it('should render message', done => {
    component.create(bootstrap).then(() => {
      done();
    }).catch(e => {
      fail(e);
      done();
    });
  });
});
