import {
  Directive,
  ElementRef,
  EventEmitter,
  HostListener,
  Output
} from '@angular/core';

@Directive({
  selector: '[appNumberOnly]'
})
export class NumberOnlyDirective {
  @Output() valueChange = new EventEmitter();
  constructor(private el: ElementRef) {}

  @HostListener('input', ['$event']) onInputChange(event: {
    stopPropagation: () => void;
  }) {
    const initalValue = this.el.nativeElement.value;
    const newValue = initalValue.replace(/[^0-9]*/g, '');
    this.el.nativeElement.value = newValue;
    this.valueChange.emit(newValue);
    if (initalValue !== this.el.nativeElement.value) {
      event.stopPropagation();
    }
  }
}
