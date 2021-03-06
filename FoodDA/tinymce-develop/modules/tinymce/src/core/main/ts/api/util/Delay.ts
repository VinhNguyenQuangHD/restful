/**
 * Copyright (c) Tiny Technologies, Inc. All rights reserved.
 * Licensed under the LGPL or a commercial license.
 * For LGPL see License.txt in the project root for license information.
 * For commercial licenses see https://www.tiny.cloud/
 */

import { Type } from '@ephox/katamari';

import Editor from '../Editor';

interface Delay {
  setEditorInterval: (editor: Editor, callback: () => void, time?: number) => number;
  setEditorTimeout: (editor: Editor, callback: () => void, time?: number) => number;
}

/**
 * Utility class for working with delayed actions like setTimeout.
 *
 * @class tinymce.util.Delay
 */

const wrappedSetTimeout = (callback: () => void, time?: number) => {
  if (!Type.isNumber(time)) {
    time = 0;
  }

  return setTimeout(callback, time);
};

const wrappedSetInterval = (callback: Function, time?: number): number => {
  if (!Type.isNumber(time)) {
    time = 1; // IE 8 needs it to be > 0
  }

  return setInterval(callback, time);
};

const Delay: Delay = {
  /**
   * Sets an editor timeout it's similar to setTimeout except that it checks if the editor instance is
   * still alive when the callback gets executed.
   *
   * @method setEditorTimeout
   * @param {tinymce.Editor} editor Editor instance to check the removed state on.
   * @param {function} callback Callback to execute when timer runs out.
   * @param {Number} time Optional time to wait before the callback is executed, defaults to 0.
   * @return {Number} Timeout id number.
   */
  setEditorTimeout: (editor, callback, time?) => {
    return wrappedSetTimeout(() => {
      if (!editor.removed) {
        callback();
      }
    }, time);
  },

  /**
   * Sets an interval timer it's similar to setInterval except that it checks if the editor instance is
   * still alive when the callback gets executed.
   *
   * @method setEditorInterval
   * @param {function} callback Callback to execute when interval time runs out.
   * @param {Number} time Optional time to wait before the callback is executed, defaults to 0.
   * @return {Number} Timeout id number.
   */
  setEditorInterval: (editor, callback, time?) => {
    const timer = wrappedSetInterval(() => {
      if (!editor.removed) {
        callback();
      } else {
        clearInterval(timer);
      }
    }, time);

    return timer;
  }
};

export default Delay;
