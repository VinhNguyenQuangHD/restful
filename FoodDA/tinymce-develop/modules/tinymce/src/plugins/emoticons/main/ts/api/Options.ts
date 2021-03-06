/**
 * Copyright (c) Tiny Technologies, Inc. All rights reserved.
 * Licensed under the LGPL or a commercial license.
 * For LGPL see License.txt in the project root for license information.
 * For commercial licenses see https://www.tiny.cloud/
 */

import Editor from 'tinymce/core/api/Editor';
import { EditorOptions } from 'tinymce/core/api/OptionTypes';

export interface UserEmojiEntry {
  readonly keywords?: string[];
  readonly char: string;
  readonly category?: string;
}

const DEFAULT_ID = 'tinymce.plugins.emoticons';

const option: {
  <K extends keyof EditorOptions>(name: K): (editor: Editor) => EditorOptions[K] | undefined;
  <T>(name: string): (editor: Editor) => T | undefined;
} = (name: string) => (editor: Editor) =>
  editor.options.get(name);

const register = (editor: Editor, pluginUrl: string): void => {
  const registerOption = editor.options.register;

  registerOption('emoticons_database', {
    processor: 'string',
    default: 'emojis'
  });

  registerOption('emoticons_database_url', {
    processor: 'string',
    default: `${pluginUrl}/js/${getEmoticonDatabase(editor)}${editor.suffix}.js`
  });

  registerOption('emoticons_database_id', {
    processor: 'string',
    default: DEFAULT_ID
  });

  registerOption('emoticons_append', {
    processor: 'object',
    default: {}
  });

  registerOption('emoticons_images_url', {
    processor: 'string',
    default: 'https://twemoji.maxcdn.com/v/13.0.1/72x72/'
  });
};

const getEmoticonDatabase = option<string>('emoticons_database');
const getEmoticonDatabaseUrl = option<string>('emoticons_database_url');
const getEmoticonDatabaseId = option<string>('emoticons_database_id');
const getAppendedEmoticons = option<Record<string, UserEmojiEntry>>('emoticons_append');
const getEmotionsImageUrl = option('emoticons_images_url');

export {
  register,
  getEmoticonDatabase,
  getEmoticonDatabaseUrl,
  getEmoticonDatabaseId,
  getAppendedEmoticons,
  getEmotionsImageUrl
};
