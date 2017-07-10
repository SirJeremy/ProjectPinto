using UnityEngine;
using System.Collections.Generic;
[System.Serializable]
public class TileSetData {
    public int height = 2;
    public int width = 2;
    public List<TileDataColumn> rows;

    public TileSetData(int width, int height) {
        //initailize list
        rows = new List<TileDataColumn>();
        //bound and set width and height
        if(width < 0)
            this.width = 1;
        else
            this.width = width;
        if(height < 0)
            this.height = 1;
        else
            this.height = height;
        //set capacity if necessary
        if(rows.Capacity < this.width)
            rows.Capacity = this.width;
        //add the rows
        for(int i = 0; i < width; i++) {
            rows.Add(new TileDataColumn(this.height));
        }
    }
    public TileSetData(TileSetData toCopy) {
        rows = new List<TileDataColumn>(toCopy.rows);
        height = toCopy.height;
        width = toCopy.width;
    }

    public void UpdateTableSize(int newWidth, int newHeight) {
        //clamp min of new width and height to 1
        if(newWidth < 0)
            newWidth = 1;
        if(newHeight < 0)
            newHeight = 1;

        //check if width and/or height is different
        bool updateWidth = width != newWidth;
        bool updateHeight = height != newHeight;

        if(updateWidth) { //if width is different
            int widthDifference = newWidth - width; //find the amount of difference in width
            if(widthDifference > 0) { //if the difference is positive
                //check if capacity is lower than width for optimization
                if(rows.Capacity < newWidth)
                    rows.Capacity = newWidth;
                //add rows for the amount of difference
                for(int i = 0; i < widthDifference; i++) {
                    rows.Add(new TileDataColumn(newHeight));
                }
                if(updateHeight) { //if height is different
                    //call ChangeHeight() on all pre-existing rows (rows that weren't just made to desigred height)
                    for(int i = 0; i < width; i++) {
                        rows[i].ChangeHeight(newHeight);
                    }
                }
            }
            else if(widthDifference < 0) { //else if width is negative
                //remove rows at the end of the list till current width is == to newWidth
                for(int i = width - 1; i >= newWidth; i--) {
                    rows.RemoveAt(i);
                }
                if(updateHeight) { //if height is different
                    //call ChangeHeight() on all remaing rows
                    for(int i = 0; i < newWidth; i++) {
                        rows[i].ChangeHeight(newHeight);
                    }
                }
            }
            //width difference != 0 bcause updateWidth would be false, it has to be true to be here
        }
        else if(updateHeight) { //else if height is different
            //call ChangeHeight() on all rows
            for(int i = 0; i < width; i++) {
                rows[i].ChangeHeight(newHeight);
            }
        }

        //lastly update stored height and width
        height = newHeight;
        width = newWidth;
    }

    public ETile[,] To2DArray() {
        if(width != rows.Count) {
            Debug.Log("Width size is not the same! " + width + "  " + rows.Count);
            return null;
        }
        ETile[,] array = new ETile[width, height];
        for(int i = 0; i < width; i++) {
            if(height != rows[i].column.Count) {
                Debug.Log("Height size is not the same! " + height + "  " + rows[i].column.Count);
                return null;
            }
            for(int j = 0; j < height; j++) {
                array[i,j] = rows[i].column[j];
            }
        }
        return array;
    }
}

[System.Serializable]
public class TileDataColumn {
    public List<ETile> column;

    public TileDataColumn(int height) {
        if(height < 0)
            height = 1;
        column = new List<ETile>();
        if(height > column.Capacity)
            column.Capacity = height;
        for(int i = 0; i < height; i++) {
            column.Add(ETile.EMPTY);
        }
    }

    public void ChangeHeight(int height) {
        //clamp min to 1
        if(height < 0)
            height = 1;
        int difference = height - column.Count; //find difference in height
        if(difference > 0) { //if difference is positive
                             //check if capacity is lower than height for optimization
            if(column.Capacity < height)
                column.Capacity = height;
            //add column items for the amount of difference
            for(int i = 0; i < difference; i++) {
                column.Add(ETile.EMPTY);
            }
        }
        else if(difference < 0) { //else if difference is negative
                                  //remove elements from end of list till new height is reached
            for(int i = column.Count - 1; i >= height; i--) {
                column.RemoveAt(i);
            }
        }
        //else == 0, do nothing; is the same height
    }
}
