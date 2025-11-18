using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BE;

namespace Proyecto_IS_Sistema_De_Tickets
{
    internal static class PermisoTreeHelper
    {
        public static void PoblarTree(TreeView tree, IEnumerable<PermisoNodo> permisos, Func<int, object> tagFactory = null)
        {
            if (tree == null) return;

            tree.BeginUpdate();
            try
            {
                tree.Nodes.Clear();
                if (permisos != null)
                {
                    foreach (var permiso in permisos)
                        tree.Nodes.Add(CrearNodo(permiso, tagFactory));
                }
            }
            finally
            {
                tree.EndUpdate();
            }

            tree.ExpandAll();
        }

        public static TreeNode CrearNodo(PermisoNodo permiso, Func<int, object> tagFactory = null)
        {
            if (permiso == null) throw new ArgumentNullException(nameof(permiso));

            var nodo = new TreeNode(permiso.Nombre)
            {
                Tag = tagFactory?.Invoke(permiso.Id) ?? ($"PERM_{permiso.Id}")
            };

            if (permiso.Hijos != null)
            {
                foreach (var hijo in permiso.Hijos)
                    nodo.Nodes.Add(CrearNodo(hijo, tagFactory));
            }

            return nodo;
        }

        public static TreeNode CrearNodo(PermisoComponent permiso, Func<int, object> tagFactory = null)
        {
            if (permiso == null) throw new ArgumentNullException(nameof(permiso));

            var nodo = new TreeNode(permiso.Nombre)
            {
                Tag = tagFactory?.Invoke(permiso.Id) ?? ($"PERM_{permiso.Id}")
            };

            if (permiso is PermisoCompuesto compuesto)
            {
                foreach (var hijo in compuesto.Hijos)
                    nodo.Nodes.Add(CrearNodo(hijo, tagFactory));
            }

            return nodo;
        }
    }
}
