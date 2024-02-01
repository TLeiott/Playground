using Hmd.Core.UI.Dialogs;
using Hmd.Core.UI.ViewModels;
using Serientermine.Series;
using Serientermine.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Serientermine.UI
{
    public static class DialogStarter
    {
        public static Window GetMainWindow()
        {
            return new MainWindow { DataContext = new MainViewModel() };
        }

        internal static ISerie ShowCreateSerieDialog(this IHmdDialogService service, IViewModel parent)
        {
            var model = new EditSerieViewModel(parent);
            var dialog = new EditSerieDialog { DataContext = model };

            service.ShowDialog(parent, dialog);

            return model.HasSavedChanges ? model.SavedSerie : null;
        }

        internal static bool ShowEditSerieDialog(this IHmdDialogService service, IViewModel parent, ISerie serie)
        {
            if (serie is null)
                throw new ArgumentNullException(nameof(serie));

            var model = new EditSerieViewModel(parent, serie);
            var dialog = new EditSerieDialog { DataContext = model };

            service.ShowDialog(parent, dialog);

            return model.HasSavedChanges;
        }
    }
}