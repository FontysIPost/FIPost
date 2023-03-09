import  { ColumnType } from "@/classes/table/ColumnType";

export interface TableCell {
  id: string,
  displayName: string,
  type: ColumnType
}