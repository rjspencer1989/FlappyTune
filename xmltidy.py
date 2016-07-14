#!/usr/bin/env python
import xmltodict
import json
import argparse
import os.path

doc = None

def main(arguments):
    notes = {"notes": []}
    input_file = arguments.inputfile
    f, e = os.path.splitext(input_file)
    output_file = '%s.json' % (f)

    print output_file
    with open(input_file) as fh:
        doc = xmltodict.parse(fh.read())
    if doc is not None:
        root = doc['score-partwise']

        parts = root['part']
        measures = parts['measure']
        for bar in measures:
            for note in bar['note']:
                if 'voice' in note:
                    del note['voice']
                if 'stem' in note:
                    del note['stem']
                if '@default-x' in note:
                    del note['@default-x']
                if '@default-y' in note:
                    del note['@default-y']
                notes["notes"].append(note)
        
        out = json.dumps(notes)
        out = out.replace("@", "")
        out = out.replace("-", "_")
        with open(output_file, 'w') as fh:
            fh.write(out)

if __name__ == '__main__':
    parser = argparse.ArgumentParser()
    parser.add_argument('inputfile')
    args = parser.parse_args()
    main(args)
