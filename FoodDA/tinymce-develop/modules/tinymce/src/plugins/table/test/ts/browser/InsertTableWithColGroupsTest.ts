import { describe, it } from '@ephox/bedrock-client';
import { TinyHooks } from '@ephox/wrap-mcagar';

import Editor from 'tinymce/core/api/Editor';
import Plugin from 'tinymce/plugins/table/Plugin';

import { insertTableTest } from '../module/test/TableTestUtils';

describe('browser.tinymce.plugins.table.InsertTableWithColGroupsTest', () => {
  const hook = TinyHooks.bddSetup<Editor>({
    plugins: 'table',
    indent: false,
    valid_styles: {
      '*': 'width,height,vertical-align,text-align,float,border-color,' +
           'background-color,border,padding,border-spacing,border-collapse'
    },
    base_url: '/project/tinymce/js/tinymce',
    statusbar: false,
    table_use_colgroups: true
  }, [ Plugin ]);

  it('TINY-6050: insert 2x2 table', () =>
    insertTableTest(hook.editor(), 2, 2, [
      [ 50, 50 ],
      [ 50, 50 ]
    ], true)
  );

  it('TINY-6050: insert 1x2 table', () =>
    insertTableTest(hook.editor(), 1, 2, [
      [ 100 ],
      [ 100 ]
    ], true)
  );

  it('TINY-6050: insert 2x1 table', () =>
    insertTableTest(hook.editor(), 2, 1, [
      [ 50, 50 ]
    ], true)
  );
});
